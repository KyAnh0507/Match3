using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : MonoBehaviour
{
    public List<SpriteRenderer> allRends = new List<SpriteRenderer>();

    public List<Screw> screws;
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
            hinge.connectedBody = screws[a].rb;
            hinge.anchor = new Vector3(screws[a].anchorX, screws[a].anchorY, 0);
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        if (HoleHasScrew() == 0)
        {
            if (hinge != null)
            {
                // Xóa component HingeJoint khỏi đối tượng
                Destroy(hinge);
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
        for (int i = screws.Count - 1; i >= 0; i--)
        {
            if (screws[i].hasScrew)
            {
                d++;
            }
        }
        return d;
    }

    public int FindHoleHasScrew()
    {
        for (int i = screws.Count - 1; i >= 0; i--)
        {
            if (screws[i].hasScrew)
            {
                return i;
            }
        }
        return -1;
    }
    public Screw GetScrew_Hole(Screw screw)
    {
        for (int i = 0; i < screws.Count; i++)
        {
            if (screws[i] == screw)
            {
                return screws[i];
            }
        }
        return null;
    }

    public void UpdateSkin(Material newMaterial)
    {
        for (int i = 0; i < allRends.Count; i++)
        {
            allRends[i].material = newMaterial;
        }
    }
}
