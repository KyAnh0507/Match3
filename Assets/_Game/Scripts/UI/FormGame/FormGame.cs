using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormGame : MonoBehaviour
{
    public bool isPauseGame;

    public Text timeLevel;
    public bool hasTime = false;

    public Image background;
    public Text textLevel;
    public CoinUI coinUI;
    public GemUI gemUI;

    public PopupLose popupLose;
    public PopupWin popupWin;
    public PopupShop popupShop;
    public PopupSetting popupSetting;
    public PopupPauseGame popupPauseGame;

    public Booster boosterAdd1;
    public Booster boosterDelete;
    public Booster boosterShuffle;
    public Booster boosterUndo;

    private void OnEnable()
    {
        LoadBooster();
        LoadTextCoin();
        //background.sprite = GameConfig.Ins.themeGames[DataManager.Ins.dataSaved.theme].sprites[2];
        textLevel.text = Constant.LEVEL + " " + (DataManager.Ins.dataSaved.indexLevel + 1).ToString();
        StartCoroutine(SetTimeLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SetTimeLevel()
    {
        yield return new WaitUntil(() => LevelManager.Ins.currentLevel != null);
        bool redTime = false;
        if (LevelManager.Ins.currentLevel.timeLevel > 10)
        {
            hasTime = true;
            timeLevel.gameObject.SetActive(true);
            timeLevel.text = Calculater.CalculaterTime(LevelManager.Ins.currentLevel.timeLevel);
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(1f);
            DOVirtual.Float(LevelManager.Ins.currentLevel.timeLevel, 0, LevelManager.Ins.currentLevel.timeLevel, (value) =>
            {
                timeLevel.text = Calculater.CalculaterTime(value);
                if (!redTime && value < 6f)
                {
                    redTime = true;
                    timeLevel.transform.DOScale(1.2f * Vector3.one, 0.5f).SetEase(Ease.Linear).SetLoops(12, LoopType.Yoyo);
                    timeLevel.DOColor(Color.red, 0.5f).SetEase(Ease.Linear).SetLoops(12, LoopType.Yoyo);
                }
            }).SetEase(Ease.Linear).OnComplete(() =>
            {
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    LevelManager.Ins.Defeat();
                });
            });
        }
        else
        {
            hasTime = false;
            timeLevel.gameObject.SetActive(false);
        }
    }

    public void PauseGame()
    {
        isPauseGame = true;
    }

    public void ResumeGame()
    {
        isPauseGame = false;
    }

    public void LoadTextCoin()
    {
        coinUI.ChangeValueImmediately(DataManager.Ins.dataSaved.coin);
        gemUI.ChangeValueImmediately(DataManager.Ins.dataSaved.gems);
    }

    public void LoadBooster()
    {
        boosterAdd1.ActiceBoosster(boosterAdd1.button.interactable, DataManager.Ins.dataSaved.boosterAdd1);
        boosterDelete.ActiceBoosster(true, DataManager.Ins.dataSaved.boosterBomb);
        boosterShuffle.ActiceBoosster(true, DataManager.Ins.dataSaved.boosterSuffer);
        DOVirtual.DelayedCall(0.1f, () =>
        {
            boosterUndo.ActiceBoosster(LevelManager.Ins.currentLevel.queueTile.matches.Count > 0, DataManager.Ins.dataSaved.boosterUndo);
        });
    }

    #region Open Popup
    public void OpenPopupLose()
    {
        PauseGame();
        popupLose.gameObject.SetActive(true);

    }

    public void OpenPopupWin()
    {
        PauseGame();
        popupWin.gameObject.SetActive(true);

        SoundManager.Ins.ChangeSound(SoundType.GAME_WIN);
    }

    public void OpenPopupPause()
    {
        PauseGame();
        popupPauseGame.gameObject.SetActive(true);

        SoundManager.Ins.ChangeSound(SoundType.POPUP_CLICK);
    }

    public void OpenPopupSetting()
    {
        PauseGame();
        popupSetting.gameObject.SetActive(true);
        popupSetting.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupSetting.tf.DOScale(Vector3.one, 0.5f);

        SoundManager.Ins.ChangeSound(SoundType.POPUP_CLICK);
    }

    public void OpenPopupShop()
    {
        PauseGame();
        popupShop.gameObject.SetActive(true);
        popupShop.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupShop.tf.DOScale(Vector3.one, 0.5f);

        SoundManager.Ins.ChangeSound(SoundType.POPUP_CLICK);
    }
    #endregion 

    #region booster
    public void Add1Tile()
    {
        if (DataManager.Ins.dataSaved.boosterAdd1 > 0)
        {
            LevelManager.Ins.currentLevel.queueTile.Add1Tile();
            DataManager.Ins.dataSaved.boosterAdd1--;
            boosterAdd1.ActiceBoosster(false, DataManager.Ins.dataSaved.boosterAdd1);
            LevelManager.Ins.currentLevel.queueTile.Reorder1();
            FirebaseManager.Ins.resource_spend((DataManager.Ins.dataSaved.level + 1) + "", "booster_add1", 1);
        }
    }

    public void DeleteIron()
    {
        if (DataManager.Ins.dataSaved.boosterBomb > 0)
        {
            GamePlay.Ins.isDeleteIron = true;
            DataManager.Ins.dataSaved.boosterBomb--;
            boosterDelete.ActiceBoosster(true, DataManager.Ins.dataSaved.boosterBomb);
            FirebaseManager.Ins.resource_spend((DataManager.Ins.dataSaved.level + 1) + "", "booster_delete", 1);
        }
    }

    public void ShuffleScrew()
    {
        if (DataManager.Ins.dataSaved.boosterSuffer > 0)
        {
            LevelManager.Ins.currentLevel.ShufflerScrew();
            DataManager.Ins.dataSaved.boosterSuffer--;
            boosterShuffle.ActiceBoosster(true, DataManager.Ins.dataSaved.boosterSuffer);

            FirebaseManager.Ins.resource_spend((DataManager.Ins.dataSaved.level + 1) + "", "booster_shuffle", 1);
        }
    }

    public void Undo()
    {
        if (DataManager.Ins.dataSaved.boosterUndo > 0 && LevelManager.Ins.currentLevel.Undo())
        {
            DataManager.Ins.dataSaved.boosterUndo--;
            boosterUndo.ActiceBoosster(LevelManager.Ins.currentLevel.queueTile.matches.Count > 0, DataManager.Ins.dataSaved.boosterUndo);
            FirebaseManager.Ins.resource_spend((DataManager.Ins.dataSaved.level + 1) + "", "booster_undo", 1);

        }
    }
    #endregion
}
