using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIWinColorPencil : UICanvas
{
    public CanvasGroup canvasGroup;

    public void OnEnable()
    {
        DataManager.Ins.dataSaved.completeChallenge = true;
        canvasGroup.DOFade(1, 0.5f);
        DataManager.Ins.dataSaved.statusDays[DataManager.Ins.dataSaved.indexCurrentDay] = true;
        DOVirtual.DelayedCall(2f, () =>
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
