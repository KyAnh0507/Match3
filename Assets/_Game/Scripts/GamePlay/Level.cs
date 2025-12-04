using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public int timeLevel = 0;
    public Transform ironParent;

    public List<UndoModel> undoRecords = new List<UndoModel>();
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
            DOVirtual.DelayedCall(0.5f, () =>
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
    public void RecordUndo(Screw screw)
    {
        UndoModel undoRecord = new UndoModel();
        undoRecord.screwUndo = screw;
        undoRecord.sortingOrder = screw.layer;
        undoRecords.Add(undoRecord);
    }

    public bool Undo()
    {
        return TryUndo();
    }

    public bool TryUndo()
    {
        if (undoRecords.Count == 0)
        {
            return false;
        }
        if (undoRecords[undoRecords.Count - 1].screwUndo == null || undoRecords[undoRecords.Count - 1].screwUndo.IsMoving ||
            !queueTile.TryUndo(undoRecords[undoRecords.Count - 1].screwUndo))
        {
            bool b = true;
            do
            {
                if (undoRecords[undoRecords.Count - 1].screwUndo == null || undoRecords[undoRecords.Count - 1].screwUndo.IsMoving || undoRecords.Count == 0)
                {
                    b = false;
                    break;
                }
                else if (undoRecords.Count > 0)
                {
                    undoRecords.RemoveAt(undoRecords.Count - 1);
                    if (undoRecords.Count == 0)
                    {
                        b = false;
                        break;
                    }
                }
            } while (!queueTile.TryUndo(undoRecords[undoRecords.Count - 1].screwUndo));
            if (!b) return false;
        }
        if (false)
        {

        }
        else
        {
            UndoManager.Ins.Undo(undoRecords[undoRecords.Count-1].screwUndo);
            Screw screw = undoRecords[undoRecords.Count - 1].screwUndo;
            screw.ChangeLayer(undoRecords[undoRecords.Count - 1].sortingOrder);
            undoRecords.RemoveAt(undoRecords.Count - 1);
        }

        return true;
    }
}
