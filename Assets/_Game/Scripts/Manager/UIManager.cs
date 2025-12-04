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

    public GameObject blockPanel;
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

    public void LoadBackground()
    {
        if (formGame != null)
        {
            formGame.background.sprite = GameConfig.Ins.themeGames[DataManager.Ins.dataSaved.theme].sprites[2];
        }
        if (formHome != null)
        {
            formHome.background.sprite = GameConfig.Ins.themeGames[DataManager.Ins.dataSaved.theme].sprites[1];
        }
    }

    public void LoadTextCoin()
    {
        if (formGame != null)
        {
            formGame.LoadTextCoin();
        }
        if (formHome != null)
        {
            formHome.LoadTextCoin();
        }
    }

    public void SetActiveBlock(bool b)
    {
        blockPanel.SetActive(b);
    }
}