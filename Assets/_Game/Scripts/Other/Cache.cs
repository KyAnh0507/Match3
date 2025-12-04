using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider2D, Iron> irons = new Dictionary<Collider2D, Iron>();

    public static Iron GetIronBySphere(Collider2D collider2D)
    {
        if (!irons.ContainsKey(collider2D))
        {
            irons.Add(collider2D, collider2D.transform.parent.GetComponent<Iron>());
        }

        return irons[collider2D];
    }
    public static Iron GetIron(Collider2D collider2D)
    {
        if (!irons.ContainsKey(collider2D))
        {
            irons.Add(collider2D, collider2D.GetComponent<Iron>());
        }

        return irons[collider2D];
    }

    private static Dictionary<Collider2D, Screw> screws = new Dictionary<Collider2D, Screw>();
    public static Screw GetScrew(Collider2D collider2D)
    {
        if (!screws.ContainsKey(collider2D))
        {
            screws.Add(collider2D, collider2D.GetComponent<Screw>());
        }

        return screws[collider2D];
    }

    private static Dictionary<Collider2D, Hole1Iron> hole1Irons = new Dictionary<Collider2D, Hole1Iron>();
    public static Hole1Iron GetHole(Collider2D collider2D)
    {
        if (!hole1Irons.ContainsKey(collider2D))
        {
            hole1Irons.Add(collider2D, collider2D.GetComponent<Hole1Iron>());
        }

        return hole1Irons[collider2D];
    }

    private static Dictionary<Collider2D, BoxPencil> boxPencils = new Dictionary<Collider2D, BoxPencil>();
    public static BoxPencil GetBoxPencil(Collider2D collider2D)
    {
        if (!boxPencils.ContainsKey(collider2D))
        {
            boxPencils.Add(collider2D, collider2D.GetComponent<BoxPencil>());
        }

        return boxPencils[collider2D];
    }
}
