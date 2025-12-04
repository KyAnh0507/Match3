using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWin : MonoBehaviour
{
    public Transform arrow;
    // Start is called before the first frame update
    void OnEnable()
    {
        arrow.rotation = Quaternion.Euler(new Vector3(0, 0, 89f));
        RotateArrow(-89f);

    }

    public void RotateArrow(float z)
    {
        arrow.DORotate(new Vector3(0, 0, z), 0.75f).OnComplete(() =>
        {
            RotateArrow(-z);
        });
    }
}
