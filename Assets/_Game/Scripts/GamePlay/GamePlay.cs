using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : Singleton<GamePlay>
{
    public List<Color> color;
    public LayerMask ironLayerMask;
    public LayerMask screwLayerMask;
    public LayerMask lockLayerMask;

    public float radiusHole = 0.2f;

    public bool blockPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !blockPlay && !UIManager.Ins.formGame.isPauseGame)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] scols = Physics2D.OverlapPointAll(mousePosition, screwLayerMask);

            if (scols.Length > 0)
            {
                (Screw, int) maxLayerScrew = (null, -1);
                int maxLayerIron = -1;
                Screw s = null;
                for (int i = 0; i < scols.Length; i++)
                {
                    s = Cache.GetScrew(scols[i]);
                    if (s != null && s.layer > maxLayerScrew.Item2)
                    {
                        maxLayerScrew = (s, s.layer);
                    }
                }

                Collider2D[] icols = Physics2D.OverlapCircleAll(mousePosition, radiusHole, ironLayerMask);

                if (icols.Length > 0)
                {
                    for (int i = 0; i < icols.Length; i++)
                    {
                        Iron ir = Cache.GetIron(icols[i]);
                        if (s != null && s.layer > maxLayerIron)
                        {
                            maxLayerIron = ir.layer;
                        }
                    }
                }
                if (maxLayerScrew.Item2 >= maxLayerIron)
                {
                    SelevtedScrew(maxLayerScrew.Item1);
                }
            }
        }
    }


    public void SelevtedScrew(Screw screw)
    {
        screw.canPlay = false;
        screw.collider2D.isTrigger= true;
        LevelManager.Ins.currentLevel.IronRemoveScrew(screw);
        LevelManager.Ins.currentLevel.queueTile.AddScrew(screw);
    }
}
