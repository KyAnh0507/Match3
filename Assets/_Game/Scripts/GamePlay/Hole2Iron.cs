using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole2Iron : MonoBehaviour
{
    public int screwType;
    public Transform tf;
    public Screw screw;
    public int layer;

    public void OnInit(Level level)
    {
        screw = SimplePool.Spawn<Screw>(PoolType.Screw, transform.position, Quaternion.identity);
        screw.ChangeScrewType(screwType);
        screw.OnInit(layer);
        level.screws.Add(screw);
    }

    public void SetScale(float x, float y)
    {
        tf.localScale = new Vector3(x, y, 1);
    }
    public void SetRotation()
    {
        tf.localRotation = Quaternion.identity;
    }
}
