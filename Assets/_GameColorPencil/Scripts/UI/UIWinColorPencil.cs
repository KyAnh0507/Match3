using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIWinColoPencil : UICanvas
{
    public CanvasGroup canvasGroup;

    public void OnEnable()
    {
        DOVirtual.DelayedCall(5f, () =>
        {

        }).OnComplete(() =>
        {
            canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
            {
                UIManagerColorPencil.Ins.ChangeScene(Scene.Home);
            });
        });
    }
}
