using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayColorPencil : Singleton<GamePlayColorPencil>
{
    public LayerMask boxPencilLayer;

    public BoxPencil currentBoxPencil;

    public Vector3 selectPosition = new Vector3(0.3f, -0.4f, 0);
    public Vector3 movePosition = new Vector3(1.08f, -1.1f, 0);

    public float distance = 1.038f;

    public bool canPlay;

    public bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlay && !isPause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D bpcol = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), boxPencilLayer);

                if (currentBoxPencil == null)
                {
                    if (bpcol != null)
                    {
                        BoxPencil boxPencil = Cache.GetBoxPencil(bpcol);
                        if (boxPencil.hasPencil)
                        {
                            currentBoxPencil = Cache.GetBoxPencil(bpcol);
                            currentBoxPencil.SelectPencil();
                        }                       
                    }
                }
                else
                {
                    if (bpcol != null)
                    {
                        BoxPencil boxPencil = Cache.GetBoxPencil(bpcol);
                        if (!boxPencil.hasPencil)
                        {
                            currentBoxPencil.pencil.Move(currentBoxPencil.TF.position, 
                                                         boxPencil.TF.position,
                                                         Mathf.Max(currentBoxPencil.layer, boxPencil.layer) + 100,
                                                         boxPencil.layer);

                            boxPencil.SetPencil(currentBoxPencil.pencil);
                            boxPencil.hasPencil = true;
                            currentBoxPencil.SetPencil(null);
                            currentBoxPencil.hasPencil = false;
                            //boxPencil.SetLayerPencil();
                        }
                        else
                        {
                            currentBoxPencil.UnSelectPencil();
                        }
                        ResetBoxPencil();
                    }
                    else
                    {
                        currentBoxPencil.UnSelectPencil();
                        ResetBoxPencil();
                    }
                }
            }          
        }
    }

    public void ResetBoxPencil()
    {
        currentBoxPencil = null;
    }
}
