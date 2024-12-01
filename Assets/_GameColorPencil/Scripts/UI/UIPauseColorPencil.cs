using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseColorPencil : UICanvas
{
    public RectTransform tf;
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void OnEnable()
    {
        GamePlayColorPencil.Ins.isPause = true;
        tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        tf.DOScale(Vector3.one, 0.5f);
    }

    public void Home()
    {
        DataManager.Ins.dataSaved.completeChallenge = true;
        canvasGroup.DOFade(1, 0.5f);
        canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            UIManagerColorPencil.Ins.ChangeScene(Scene.Home);
        });
    }

    public void Continue()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
        GamePlayColorPencil.Ins.isPause = false;
    }
}
