using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoading : UICanvas
{
    public AnimationCurve animationCurve;
    public RectTransform rect;
    public float duration = 2f;

    public CanvasGroup canvasGroup;

    public void OpenUILoading()
    {
        DOVirtual.Float(0f, 1, duration, value =>
        {
            rect.localScale = new Vector3(value, 1, 1);
        }).SetEase(animationCurve).OnComplete(() =>
        {
            canvasGroup.DOFade(0, 0.7f).OnComplete(() =>
            {
                UIManagerColorPencil.Ins.CloseUI<UILoading>();
                UIManagerColorPencil.Ins.OpenUI<UIGamePlay>().OpenUIGamePlay();
            });          
        });
    }
}
