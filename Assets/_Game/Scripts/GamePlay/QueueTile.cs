using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueTile : MonoBehaviour
{
    public Transform tf;
    public List<Tile> tiles;
    public int numberTile;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            Tile t = SimplePool.Spawn<Tile>(PoolType.Tile, tf);
            if (numberTile%2 == 0)
            {
                t.transform.localPosition = new Vector3(-numberTile / 2 + i + 0.5f, 0, 0);

            }
            else
            {
                t.transform.localPosition = new Vector3(-numberTile / 2 + i, 0, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
