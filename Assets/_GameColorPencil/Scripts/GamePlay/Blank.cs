using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blank : MonoBehaviour
{
    public Transform tf;
    public SpriteRenderer sprite;

    public void OnInit(int n)
    {
        sprite.sortingOrder = n;
    }
}
