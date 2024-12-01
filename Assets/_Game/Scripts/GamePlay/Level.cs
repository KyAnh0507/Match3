using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class Level : MonoBehaviour
{
    public QueueTile queueTile;

    public List<Screw> screws;
    public List<Iron> irons;
    public List<Hole1Iron> hole1Irons;
    public List<Hole2Iron> hole2Irons;

    public Hole1Iron hole1Iron;

    public bool isDefeatChecked;
    public bool canWin;
    public bool canLose;
    public int numberColor;
    [Header("Super Hard")]
    public int superHardTime;

    public int targetMatch;
    public int numbermatched = 0;

    public void OnInit()
    {
        queueTile.OnInit();

        for (int j = 0; j < irons.Count; j++)
        {
            for (int i = 0; i < irons[j].hole1Irons.Count; i++)
            {
                irons[j].hole1Irons[i].layer = irons[j].layer;
                irons[j].hole1Irons[i].OnInit(this);

                Screw_Hole screw_Hole = new Screw_Hole(irons[j].hole1Irons[i].screw, irons[j].hole1Irons[i]);
                irons[j].screws_holes.Add(screw_Hole);
            }
        }

        for (int i = 0; i < hole2Irons.Count; i++)
        {
            int maxLayer = 1;
            hole2Irons[i].OnInit(this);

            Collider2D[] icols = Physics2D.OverlapPointAll(hole2Irons[i].transform.position, GamePlay.Ins.ironLayerMask);
            if (icols.Length > 0)
            {
                int n = icols.Length;
                for (int j = 0; j < n; j++)
                {
                    Iron iron = Cache.GetIron(icols[j]);
                    if (iron != null)
                    {
                        if (iron.layer > maxLayer)
                        {
                            maxLayer = iron.layer;
                        }
                        Hole1Iron h = Instantiate(hole1Iron, hole2Irons[i].transform.position, Quaternion.identity);
                        h.SetParent(iron.transform);
                        h.SetOrderInLayer(4 + iron.layer * 2);
                        h.SetRotation();
                        h.SetScale(1, 3.333333f);
                        iron.hole1Irons.Add(h);

                        Screw_Hole screw_Hole = new Screw_Hole(hole2Irons[i].screw, h);
                        h.screw = hole2Irons[i].screw;
                        iron.screws_holes.Add(screw_Hole);
                    }
                }
            }
            hole2Irons[i].layer = maxLayer;
        }

        
        
        //queueTile
        isDefeatChecked = false;
        UndoManager.Ins.OnInit(this);

        GameManager.Ins.ChangeState(GameState.GAMEPLAY);
    }

    private void Update()
    {
        if (GameManager.Ins.IsState(GameState.GAMEPLAY))
        {
            if (CheckDefeatCondition() && !isDefeatChecked)
            {
                StartCoroutine(CheckDefeatContinuously());
            }
        }
        if (numbermatched == targetMatch && GameManager.Ins.IsState(GameState.GAMEPLAY) && canWin)
        {
            GameManager.Ins.ChangeState(GameState.FINISH);
            DOVirtual.DelayedCall(1.5f, () =>
            {
                LevelManager.Ins.Victory();
            });
        }
    }

    private IEnumerator CheckDefeatContinuously()
    {
        isDefeatChecked = true;
        float endTime = Time.time + 3f;

        while (Time.time < endTime)
        {
            if (!CheckDefeatCondition() || !isDefeatChecked)
            {
                isDefeatChecked = false;
                yield break; // Ngừng coroutine nếu điều kiện thỏa mãn
            }
            yield return null; // Chờ cho đến khung hình tiếp theo
        }
        if (LevelManager.Ins.currentLevel.isDefeatChecked)
        {
            LevelManager.Ins.Defeat();
        }
    }

    public void IronRemoveScrew(Screw screw)
    {
        for (int i = 0; i < irons.Count; i++)
        {
            Screw_Hole sh = irons[i].GetScrew_Hole(screw);
            if (sh != null)
            {
                sh.hasScrew = false;
            }
        }
    }

    public bool CheckDefeatCondition()
    {
        return queueTile.numberScrew >= queueTile.numberTile;
    }

    public void ShufflerScrew()
    {
        List<int> ints = new List<int>();
        List<int> intsShuffler = new List<int>();
        for (int i = 0; i < screws.Count; i++)
        {
            if (screws[i].canPlay)
            {
                ints.Add(i);
                intsShuffler.Add(screws[i].screwType);
            }
        }
        if (ints.Count > 1)
        {
            for (int i = 0; i < ints.Count; i++)
            {
                int j = (int)Random.Range(0, ints.Count);
                int k;
                do
                {
                    k = (int)Random.Range(0, ints.Count);
                } while (k == j);
                int tmp = intsShuffler[j];
                intsShuffler[j] = intsShuffler[k];
                intsShuffler[k] = tmp;
            }
        }
        for (int i = 0;i < ints.Count; i++)
        {
            screws[ints[i]].ChangeScrewType(intsShuffler[i]);
        }
    }

    public void Undo()
    {
        if (queueTile.numberScrew > 0)
        {
            UndoManager.Ins.Undo(queueTile.tile_screws[queueTile.numberScrew - 1].screw);
            queueTile.tile_screws[queueTile.numberScrew - 1].screw = null;
            queueTile.numberScrew--;
        }
    }
}
