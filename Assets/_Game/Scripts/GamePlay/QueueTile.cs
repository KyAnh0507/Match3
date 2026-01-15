using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class QueueTile : MonoBehaviour
{
    public Transform tf;
    public List<Tile_Screw> tile_screws;
    public int numberTile;
    public int numberScrew;
    public bool[] hasScrewType = new bool[15];
    public bool isMatching = false;

    [SerializeField, Range(2, 20)]
    public int size = 8;
    public int max;
    public List<Match> matches = new List<Match>();
    // Start is called before the first frame update
    public void OnInit()
    {
        for (int i = 0; i < numberTile; i++)
        {
            Tile t = SimplePool.Spawn<Tile>(PoolType.Tile, tf);
            if (numberTile%2 == 0)
            {
                t.transform.localPosition = new Vector3((-numberTile / 2 + i + 0.5f)/1.53f, 0, 0);
                tile_screws.Add(new Tile_Screw(t, null));

            }
            else
            {
                t.transform.localPosition = new Vector3((-numberTile / 2 + i)/1.53f, 0, 0);
                tile_screws.Add(new Tile_Screw(t, null));

            }
            t.TF.localScale = new Vector3(0.6f, 0.6f, 1);
        }
        for (int i = 0; i < 15; i++)
        {
            hasScrewType[i] = false;
        }

        max = size;

        LevelManager.Ins.currentLevel.canLose = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectScrew(Screw screw)
    {
        bool isAdded = false;
        UndoManager.Ins.AddDataUndo(screw);
        LevelManager.Ins.currentLevel.RecordUndo(screw);
        screw.SetParent(transform);
        screw.canPlay = false;
        screw.DisableCollider();
        LevelManager.Ins.currentLevel.IronRemoveScrew(screw);
        for (int i = 0; i < matches.Count; i++)
        {
            if (screw.screwType != matches[i].ScrewType || matches[i].IsFull)
            {
                continue;
            }
            matches[i].screws.Add(screw);
            Reorder(screw);
            isAdded = true;
            if (matches[i].IsFull) StartCoroutine(MatchRoutine(matches[i]));
            break;
        }

        if (!isAdded)
        {
            matches.Add(new Match(screw));
            Reorder(screw);
        }
        int d = 0;
        for (int i = 0; i < matches.Count; i++)
        {
            if (matches[i].HasScrew(screw))
            {
                int n = 0;
                for (int j = 0; j < matches[i].screws.Count; j++)
                {
                    if (matches[i].screws[j] == screw)
                    {
                        n = j;
                        break;
                    }
                }
                d += n;
                break;
            }
            d += matches[i].screws.Count;
        }
        screw.PullUp1(tile_screws[d].tile.transform.position);
        DOVirtual.DelayedCall(0.5f, () =>
        {
            UIManager.Ins.formGame.boosterUndo.ActiceBoosster(LevelManager.Ins.currentLevel.queueTile.matches.Count > 0, DataManager.Ins.dataSaved.boosterUndo);
        });
        
    }

    public bool IsFull()
    {
        int total = 0;
        for (int i = 0; i < matches.Count; i++)
        {
            total += matches[i].screws.Count;
        }
        return total >= max;
    }

    public void Add1Tile()
    {
        numberTile++;
        max++;
        for (int i = 0; i < numberTile; i++)
        {
            if (i == numberTile - 1)
            {
                Tile t = SimplePool.Spawn<Tile>(PoolType.Tile, tf);
                if (numberTile % 2 == 0)
                {
                    t.transform.localPosition = new Vector3((-numberTile / 2 + i + 0.5f) / 1.53f, 0, 0);
                    tile_screws.Add(new Tile_Screw(t, null));

                }
                else
                {
                    t.transform.localPosition = new Vector3((-numberTile / 2 + i) / 1.53f, 0, 0);
                    tile_screws.Add(new Tile_Screw(t, null));

                }
                t.TF.localScale = new Vector3(0.05f, 0.05f, 1);
                t.TF.DOScale(new Vector3(0.6f, 0.6f, 1), 0.5f);
            }
            else
            {
                if (numberTile % 2 == 0)
                {
                    Vector3 v = new Vector3((-numberTile / 2 + i + 0.5f) / 1.53f, 0, 0);
                    tile_screws[i].tile.TF.DOLocalMove(new Vector3((-numberTile / 2 + i + 0.5f) / 1.53f, 0, 0), 0.5f);
                    if (tile_screws[i].screw != null)
                    {
                        tile_screws[i].screw.TF.DOMove(tf.TransformPoint(v), 0.5f);
                    }
                }
                else
                {
                    Vector3 v = new Vector3((-numberTile / 2 + i) / 1.53f, 0, 0);
                    tile_screws[i].tile.TF.DOLocalMove(new Vector3((-numberTile / 2 + i) / 1.53f, 0, 0), 0.5f);
                    if (tile_screws[i].screw != null)
                    {
                        tile_screws[i].screw.TF.DOMove(tf.TransformPoint(v), 0.5f);
                    }
                }
            }
            
        }
    }

    public bool TryUndo(Screw screw)
    {
        int index = -1;
        for (int i = 0; i < matches.Count; i++)
        {
            if (matches[i].screws.Contains(screw))
            {
                index = i;
            }
        }
        if (index == -1/* || matches[index].IsFull*/)
        {
            return false;
        }
        matches[index].screws.Remove(screw);
        if (matches[index].IsEmpty)
        {
            matches.RemoveAt(index);
        }
        Reorder();
        return true;
    }

    public void Reorder(Screw screw = null)
    {
        List<Screw> screws = new List<Screw>();
        for (int i = 0; i < matches.Count; i++)
        {
            screws.AddRange(matches[i].screws);
        }
        if (max % 2 == 0)
        {
            for (int i = 0; i < screws.Count; i++)
            {
                if (screws[i] == screw) continue;
                screws[i].DisableCollider();
                screws[i].MoveX2(new Vector3(tile_screws[i].tile.transform.position.x , 0f, 0f));
            }
        }
        else
        {
            for (int i = 0; i < screws.Count; i++)
            {
                if (screws[i] == screw) continue;
                screws[i].DisableCollider();
                screws[i].MoveX2(new Vector3(tile_screws[i].tile.transform.position.x, 0f, 0f));
            }
        }

        if (IsFull())
        {
            StartCoroutine(LoseRoutine(screws));
        }
    }

    public void Reorder1()
    {
        List<Screw> screws = new List<Screw>();
        for (int i = 0; i < matches.Count; i++)
        {
            screws.AddRange(matches[i].screws);
        }
        if (max % 2 == 0)
        {
            for (int i = 0; i < screws.Count; i++)
            {
                screws[i].DisableCollider();

                Vector3 v = new Vector3((-numberTile / 2 + i + 0.5f) / 1.53f, 0, 0);

                screws[i].MoveX1(new Vector3((-max / 2 + i + 0.5f) / 1.53f, 0f, 0f), 0.5f);
            }
        }
        else
        {
            for (int i = 0; i < screws.Count; i++)
            {
                screws[i].DisableCollider();

                Vector3 v = new Vector3((-numberTile / 2 + i) / 1.53f, 0, 0);

                screws[i].MoveX1(new Vector3((-max / 2 + i) / 1.53f, 0f, 0f), 0.5f);
            }
        }
    }

    private IEnumerator MatchRoutine(Match match)
    {
        LevelManager.Ins.currentLevel.canWin = false;
        LevelManager.Ins.currentLevel.canLose = false;
        yield return new WaitUntil(() => match.IsMatchable());
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => match.IsMatchable());
        match.Process();
        matches.Remove(match);
        LevelManager.Ins.currentLevel.numbermatched++;
        LevelManager.Ins.currentLevel.canWin = true;
        LevelManager.Ins.currentLevel.canLose = true;
        yield return new WaitForSeconds(0.4f);
        Reorder();
    }

    private IEnumerator LoseRoutine(List<Screw> screws)
    {
        yield return new WaitUntil(() => IsStable(screws) && LevelManager.Ins.currentLevel.canLose);
        yield return new WaitForSeconds(0.5f);
        if (!IsFull())
        {
            yield break;
        }

        LevelManager.Ins.Defeat();
    }

    private bool IsStable(List<Screw> screws)
    {
        for (int i = 0; i < screws.Count; i++)
        {
            if (screws[i].IsMoving)
            {
                return false;
            }
        }
        return true;
    }


}

[System.Serializable]
public class Tile_Screw
{
    public Tile tile;
    public Screw screw;

    public Tile_Screw()
    {

    }
    public Tile_Screw(Tile tile, Screw screw)
    {
        this.tile = tile;
        this.screw = screw;
    }
}

[System.Serializable]
public struct Match
{
    public List<Screw> screws;

    public bool IsEmpty => screws.Count == 0;
    public bool IsFull => screws.Count == 3;
    public int ScrewType => screws[0].screwType;

    public Match(Screw screw)
    {
        screws = new List<Screw>();
        screws.Add(screw);
    }

    public bool IsMatchable()
    {
        for (int i = 0; i < screws.Count; i++)
        {
            if (screws[i].IsMoving)
            {
                return false;
            }
        }
        return true;
    }

    public void Process()
    {
        for (int i = 0; i < screws.Count; i++)
        {
            screws[i].Disappear();
        }
    }

    public bool HasScrew(Screw screw)
    {
        for (int i = 0; i < screws.Count; i++)
        {
            if (screws[i] == screw)
            {
                return true;
            }
        }
        return false;
    }

    public void Reorder()
    {

    }
}
