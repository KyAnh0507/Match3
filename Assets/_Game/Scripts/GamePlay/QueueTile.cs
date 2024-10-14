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
    // Start is called before the first frame update
    void Start()
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
        if (numberScrew < numberTile)
        {
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
                        DOVirtual.DelayedCall(0.8f, Match3);
                        return;
                    }
                    else if (i+1 == numberScrew)
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

    public void Match3()
    {
        for (int i = 0; i < numberScrew-2; i++)
        {
            if (tile_screws[i].screw.screwType == tile_screws[i+1].screw.screwType && tile_screws[i+1].screw.screwType == tile_screws[i+2].screw.screwType)
            {
                tile_screws[i].screw.Match3();
                tile_screws[i+1].screw.Match3();
                tile_screws[i+2].screw.Match3();
                tile_screws[i].screw = null;
                tile_screws[i+1].screw = null;
                tile_screws[i+2].screw = null;

                if (i + 3 == numberScrew) return;
                for (int j = i; j < numberScrew; j++)
                {
                    Debug.Log("hit");
                    if (j < numberScrew - 3)
                    {
                        tile_screws[j].screw = tile_screws[j + 3].screw;
                        tile_screws[j].screw.Move1(tile_screws[j].tile.TF.position);
                    }
                    else if (j >= numberScrew - 3)
                    {
                        tile_screws[j + i].screw = null;
                    }
                }
                numberScrew -= 3;
                break;
            }
        }
        
    }
    public void Add1Tile()
    {

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
