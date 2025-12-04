using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Iron : MonoBehaviour, IBaseUnitUndo
{
    public List<SpriteRenderer> allRends = new List<SpriteRenderer>();

    public List<Screw_Hole> screws_holes;
    public List<Hole1Iron> hole1Irons;
    public HingeJoint2D hinge;
    public Rigidbody2D rb;
    public SpriteRenderer spriteIron;
    public SortingGroup sortingGroup;

    public int id;
    public PolygonCollider2D polygonCollider;
    public int layer;
    public bool hasIce;
    public bool loaded = false;
    public bool isTrigger = false;

    private void OnEnable()
    {
        if (CreateLeveManager.ins != null) layer = CreateLeveManager.ins.currentLayer;
        ChangeLayer();
        
    }
    /*private void Start()
    {
        foreach (var screw_hole in screws_holes)
        {
            screw_hole.screw = screw_hole.hole.screw;
        }
    }*/

    private void Update()
    {
        if (CreateLeveManager.ins != null || !loaded) return;
        if (HoleHasScrew() == 1)
        {
            int a = FindHoleHasScrew();
            hinge.enabled = true;
            hinge.connectedBody = screws_holes[a].hole1Iron.screw.rb;
            hinge.anchor = new Vector3(screws_holes[a].anchorX, screws_holes[a].anchorY, 0);
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        if (HoleHasScrew() == 0)
        {
            if (hinge != null)
            {
                hinge.enabled = false;
            }
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        if (HoleHasScrew() >= 2)
        {
            rb.bodyType = RigidbodyType2D.Static;
            hinge.enabled = true;
        }
    }
    public int HoleHasScrew()
    {
        int d = 0;
        for (int i = screws_holes.Count - 1; i >= 0; i--)
        {
            if (screws_holes[i].hasScrew)
            {
                d++;
            }
        }
        return d;
    }

    public int FindHoleHasScrew()
    {
        for (int i = screws_holes.Count - 1; i >= 0; i--)
        {
            if (screws_holes[i].hasScrew)
            {
                return i;
            }
        }
        return -1;
    }

    public void UpdateSkin(Material newMaterial)
    {
        for (int i = 0; i < allRends.Count; i++)
        {
            allRends[i].material = newMaterial;
        }
    }

    public Screw_Hole GetScrew_Hole(Screw screw)
    {
        for (int i = 0; i < screws_holes.Count; i++)
        {
            if (screws_holes[i].screw == screw)
            {
                return screws_holes[i];
            }
        }
        return null;
    }

    public void ChangeLayer()
    {
        gameObject.layer = layer + 12;
        spriteIron.sortingOrder = layer * 10;
        sortingGroup.sortingOrder = layer * 10;
    }

    public void AddDataUndo(UndoModel undoModel)
    {
        if (!isTrigger)
        {
            IronUndoModel model = new IronUndoModel();
            model.index = UndoManager.Ins.currentCountMove;
            model.position = transform.position;
            model.rotation = transform.rotation;
            for (int i = 0; i < screws_holes.Count; i++)
            {
                model.screws_holes.Add(new Screw_Hole(screws_holes[i].screw,
                                                      screws_holes[i].hole1Iron,
                                                      screws_holes[i].anchorX,
                                                      screws_holes[i].anchorY,
                                                      screws_holes[i].hasScrew));
            }
            model.isTrigger = isTrigger;
            model.velocity = rb.velocity;

            undoModel.ironUndoModels.Add(model);
        }
        else
        {
            undoModel.ironUndoModels.Clear();
        }
    }

    public void Undo(Screw screw, UndoModel undoModel, int n)
    {
        IronUndoModel model = undoModel.ironUndoModels[n];
        if (!isTrigger)
        {
            if (!screws_holes.Any(x => x.screw == screw)) return;
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                screws_holes = model.screws_holes;
                for (int i = 0; i < screws_holes.Count; i++)
                {
                    if (!screws_holes[i].screw.canPlay || !screws_holes[i].screw.gameObject.activeSelf)
                    {
                        screws_holes[i].hasScrew = false;
                    }
                    else
                    {
                        screws_holes[i].hasScrew = true;
                    }
                }
                transform.position = model.position;
                transform.rotation = model.rotation;
                rb.bodyType = RigidbodyType2D.Static;
                rb.gravityScale = 1;
                rb.velocity = model.velocity;
            }
            else
            {
                screws_holes = model.screws_holes;
                for (int i = 0; i < screws_holes.Count; i++)
                {
                    if (!screws_holes[i].screw.canPlay || !screws_holes[i].screw.gameObject.activeSelf)
                    {
                        screws_holes[i].hasScrew = false;
                    }
                    else
                    {
                        screws_holes[i].hasScrew = true;
                    }
                }
                rb.bodyType = RigidbodyType2D.Static;
                rb.gravityScale = 1;
            }
        }
    }

    public int nIronVaCham = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CreateLeveManager.ins != null)
        {
            if (collision.collider.CompareTag("iron"))
            {
                int b = collision.collider.transform.GetComponent<Iron>().layer;
                if (b == this.layer)
                {
                    nIronVaCham++;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (CreateLeveManager.ins != null)
        {
            if (collision.collider.CompareTag("iron"))
            {
                int b = collision.collider.transform.GetComponent<Iron>().layer;
                if (b == this.layer)
                {
                    nIronVaCham++;
                }
            }
        }
    }
}

[System.Serializable]
public class Screw_Hole
{
    public Screw screw;
    public Hole1Iron hole1Iron;
    public float anchorX;
    public float anchorY;
    public bool hasScrew;

    public Screw_Hole()
    {

    }

    public Screw_Hole(Screw screw, Hole1Iron hole1Iron, bool hasScrew = true)
    {
        this.screw = screw;
        this.hole1Iron = hole1Iron;
        this.anchorX = hole1Iron.tf.localPosition.x;
        this.anchorY = hole1Iron.tf.localPosition.y;
        this.hasScrew = hasScrew;
    }

    public Screw_Hole(Screw screw, Hole1Iron hole1Iron, float anchorX, float anchorY, bool hasScrew = true)
    {
        this.screw = screw;
        this.hole1Iron = hole1Iron;
        this.anchorX = anchorX;
        this.anchorY = anchorY;
        this.hasScrew = hasScrew;
    }
}
