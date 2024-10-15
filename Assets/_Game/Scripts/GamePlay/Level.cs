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
    public int numberColor;
    [Header("Super Hard")]
    public int superHardTime;

    public int targetMatch;
    public int numbermatched = 0;

    public void OnInit()
    {
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
                        h.SetScale(1 / iron.transform.localScale.x, 1 / iron.transform.localScale.y);
                        iron.hole1Irons.Add(h);
                    }
                }
            }
            hole2Irons[i].layer = maxLayer;
        }

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
        
        //queueTile
        isDefeatChecked = false;

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
        if (numbermatched == targetMatch && GameManager.Ins.IsState(GameState.GAMEPLAY))
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
}
