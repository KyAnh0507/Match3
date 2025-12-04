using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupPauseGame : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    private void OnEnable()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1f, 0.4f);
    }
    public void Home()
    {
        UIManager.Ins.ChangeScene(Scene.Home);
        Close();
    }

    public void Continue()
    {
        UIManager.Ins.formGame.ResumeGame();
        Close();
    }

    public void Close()
    {
        canvasGroup.DOFade(0, 0.4f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
