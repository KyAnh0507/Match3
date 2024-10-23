using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueTile : MonoBehaviour
{
    public Transform tf;
    public List<Tile_Screw> tile_screws;
    public int numberTile;
    public int numberScrew;
    public bool[] hasScrewType = new bool[15];
    public bool isMatching = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScrew(Screw screw)
    {
        UndoManager.Ins.AddDataUndo(screw);
        if (numberScrew < numberTile)
        {
            screw.canPlay = false;
            screw.collider2D.isTrigger = true;
            LevelManager.Ins.currentLevel.IronRemoveScrew(screw);
            for (int i = 0; i < numberScrew; i++)
            {
                if (!hasScrewType[screw.screwType]) break;
                if (tile_screws[i].screw.screwType == screw.screwType)
                {
                    if (i + 1 < numberScrew && tile_screws[i + 1].screw.screwType == screw.screwType)
                    {
                        for (int j = numberScrew; j > i + 2; j--)
                        {
                            tile_screws[j].screw = tile_screws[j - 1].screw;
                            tile_screws[j].screw.Move1(tile_screws[j].tile.TF.position);
                        }
                        numberScrew++;
                        tile_screws[i + 2].screw = screw;
                        screw.Move(tile_screws[i + 2].tile.TF.position);
                        StartCoroutine(Match3());
                        return;
                    }
                    else if (i + 1 == numberScrew)
                    {
                        break;
                    }
                    else
                    {
                        for (int j = numberScrew; j > i + 1; j--)
                        {
                            tile_screws[j].screw = tile_screws[j- 1].screw;
                            tile_screws[j].screw.Move1(tile_screws[j].tile.TF.position);

                        }
                        numberScrew++;
                        screw.Move(tile_screws[i + 1].tile.TF.position);
                        tile_screws[i + 1].screw = screw;
                        return;
                    }
                }
            }
            hasScrewType[screw.screwType] = true;
            tile_screws[numberScrew].screw = screw;
            screw.Move(tile_screws[numberScrew].tile.TF.position);
            numberScrew++;
        }
        
    }

    public IEnumerator Match3()
    {
        yield return new WaitUntil(() => !isMatching);
        isMatching = true;
        int i;
        LevelManager.Ins.currentLevel.canWin = false;
        for (i = 0; i < numberScrew-2; i++)
        {
            if (tile_screws[i].screw.screwType == tile_screws[i+1].screw.screwType && tile_screws[i+1].screw.screwType == tile_screws[i+2].screw.screwType)
            {
                tile_screws[i].screw.Match3();
                tile_screws[i+1].screw.Match3();
                tile_screws[i+2].screw.Match3();
                
                break;
            }
        }
        yield return new WaitForSeconds(1f);
        yield return new WaitForEndOfFrame();
        Debug.Log("numberScrew: "+ numberScrew + "......" + "i: " + i);
        for (int j = i; j < numberScrew - 2; j++)
        {
            if (tile_screws[j].screw.screwType == tile_screws[j + 1].screw.screwType && tile_screws[j + 1].screw.screwType == tile_screws[j + 2].screw.screwType)
            {
                i = j;
                break;
            }
        }
        if (i + 3 != numberScrew)
        {
            for (int j = i; j < numberScrew; j++)
            {
                if (j < numberScrew - 3)
                {
                    tile_screws[j].screw = tile_screws[j + 3].screw;
                    tile_screws[j].screw.Move1(tile_screws[j].tile.TF.position);

                }
                else if (j >= numberScrew - 3)
                {
                    tile_screws[j].screw = null;
                }
            }
        }
        numberScrew -= 3;
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForEndOfFrame();
        LevelManager.Ins.currentLevel.numbermatched++;
        LevelManager.Ins.currentLevel.canWin = true;
        isMatching = false;

    }
    public void Add1Tile()
    {
        numberTile++;
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
