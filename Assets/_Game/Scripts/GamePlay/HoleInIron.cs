using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInIron : MonoBehaviour
{
    public Transform trans;
    public SpriteRenderer spriteRenderer;

    
    public void SetParent(Transform parent)
    {
        trans.SetParent(parent);
    }
    public void SetOrderInLayer(int layer)
    {
        spriteRenderer.sortingOrder = layer;
    }

    public void SetScale(float x, float y)
    {
        trans.localScale = new Vector3(x, y, 1);
    }
    public void SetRotation()
    {
        trans.localRotation = Quaternion.identity;
    }

}
