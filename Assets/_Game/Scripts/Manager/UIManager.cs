using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public FormGame formGame;
    public FormHome formHome;
    public FormLoading formLoading;

    public CanvasGroup fadePanel;
    public float timeFadeIn;
    public float timeFadeOut;
    // Start is called before the first frame update
    void OnEnable()
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.alpha = 1f;
        DOVirtual.Float(1f, 0f, timeFadeIn, value => { fadePanel.alpha = value; })
            .OnComplete(() =>
            {
                fadePanel.gameObject.SetActive(false);
            });
    }

    public void ChangeScene(Scene newScene)
    {
        DOTween.KillAll();

        fadePanel.gameObject.SetActive(true);
        fadePanel.alpha = 0f;
        DOVirtual.Float(0f, 1f, timeFadeOut, value => { fadePanel.alpha = value; })
            .OnComplete(() =>
            {
                SceneManager.LoadSceneAsync(newScene.ToString());
            });
    }
}