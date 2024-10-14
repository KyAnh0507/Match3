using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormLoading : MonoBehaviour
{
    public float loadDuration;
    public Transform fillImage;
    public AnimationCurve loadingCurve;

    private void Start()
    {
        DOVirtual.Float(0f, 1f, loadDuration, value =>
        {
            fillImage.localScale = new Vector3(value, 1, 1);
        })
        .SetEase(loadingCurve)
        .OnComplete(() =>
        {
            UIManager.Ins.ChangeScene(Scene.Game);
        });
    }
}
