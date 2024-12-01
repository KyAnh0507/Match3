using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    public Text textTime;

    public CanvasGroup canvasGroup;

    private void OnEnable()
    {
        DOVirtual.DelayedCall(2f, () =>
        {
            DOVirtual.Float(180f, 0, 180f, (value) =>
            {
                textTime.text = Calculater.CalculaterTime(value);
            }).SetUpdate(true).SetEase(Ease.Linear);
        });

    }

    public void OpenUIGamePlay()
    {
        canvasGroup.DOFade(1f, 0.7f).OnComplete(() =>
        {
            LevelManagerColorPencil.Ins.LoadLevel();
        });
    }
    public void OpenUIPause()
    {
        UIManagerColorPencil.Ins.OpenUI<UIPauseColorPencil>();
    }
    public void CloseUIGamePlay(bool b = false)
    {
        canvasGroup.DOFade(0, 0.7f).OnComplete(() =>
        {
            if (b)
            {
                OpenUIGamePlay();
            }
            else
            {
                LevelManagerColorPencil.Ins.currentLevel.Despawn();
                UIManagerColorPencil.Ins.CloseUI<UIGamePlay>();
            }           
        });
    }
}
