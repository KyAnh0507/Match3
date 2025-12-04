using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole1Iron : MonoBehaviour
{
    public Transform trans;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public int screwType;
    public bool hasScrew = true;
    public bool hasLock = false;
    public Transform tf;
    public Screw screw;
    public int layer;
    public void OnInit(Level level)
    {
        //Debug.Log("hit");

        if (hasScrew)
        {
            screw = SimplePool.Spawn<Screw>(PoolType.Screw, transform.position, Quaternion.identity);
            screw.ChangeScrewType(screwType);
            screw.OnInit(layer);
            screw.TF.SetParent(level.transform);
            screw.TF.localScale = Vector3.one;
            level.screws.Add(screw);
        }
    }

    public void OnInit()
    {
        if (hasScrew)
        {
            screw = Instantiate(CreateLeveManager.ins.screwPrefab, transform.position, Quaternion.identity);
            screw.ChangeScrewType1(screwType);
            screw.OnInit(layer);
            screw.transform.SetParent(CreateLeveManager.ins.tfScrew);
            screw.transform.localScale *= 3;
        }
    }
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
