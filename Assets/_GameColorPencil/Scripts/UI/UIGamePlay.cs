using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : UICanvas
{
    public Text textLevel;

    public CanvasGroup canvasGroup;
    public void OpenUIGamePlay()
    {
        canvasGroup.DOFade(1f, 0.7f).OnComplete(() =>
        {
            LevelManagerColorPencil.Ins.LoadLevel();
        });
        textLevel.text = Constant.LEVEL + (DataManager.Ins.dataSaved.indexLevelColorPencil + 1);
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
