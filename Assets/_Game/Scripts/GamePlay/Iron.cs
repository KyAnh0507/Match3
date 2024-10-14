using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : MonoBehaviour
{
    public List<SpriteRenderer> allRends = new List<SpriteRenderer>();

    public List<Screw_Hole> screws_holes;
    public List<Hole1Iron> hole1Irons;
    public HingeJoint2D hinge;
    public Rigidbody2D rb;

    public int layer;

    /*private void Start()
    {
        foreach (var screw_hole in screws_holes)
        {
            screw_hole.screw = screw_hole.hole.screw;
        }
    }*/

    private void Update()
    {
        if (HoleHasScrew() == 1)
        {
            int a = FindHoleHasScrew();
            hinge.connectedBody = screws_holes[a].hole1Iron.rb;
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
}
