using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormGame : MonoBehaviour
{
    public bool isPauseGame;


    public PopupLose popupLose;
    public PopupWin popupWin;

    public Booster boosterAdd1;
    public Booster boosterDelete;
    public Booster boosterShuffle;
    public Booster boosterUndo;

    private void OnEnable()
    {
        boosterAdd1.ActiceBoosster(true, DataManager.Ins.dataSaved.boosterAdd1);
        boosterDelete.ActiceBoosster(true, DataManager.Ins.dataSaved.boosterBomb);
        boosterShuffle.ActiceBoosster(true, DataManager.Ins.dataSaved.boosterSuffer);
        boosterUndo.ActiceBoosster(false, DataManager.Ins.dataSaved.boosterUndo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        isPauseGame = true;
    }

    public void ResumeGame()
    {
        isPauseGame = false;
    }
    public void OpenPopupLose()
    {
        PauseGame();
        popupLose.gameObject.SetActive(true);

    }

    public void OpenPopupWin()
    {
        PauseGame();
        popupWin.gameObject.SetActive(true);
    }

    #region booster
    public void Add1Tile()
    {
        if (DataManager.Ins.dataSaved.boosterAdd1 > 0)
        {
            LevelManager.Ins.currentLevel.queueTile.Add1Tile();
            DataManager.Ins.dataSaved.boosterAdd1--;
            boosterAdd1.ActiceBoosster(false, DataManager.Ins.dataSaved.boosterAdd1);
        }
    }

    public void DeleteIron()
    {
        if (DataManager.Ins.dataSaved.boosterBomb > 0)
        {
            GamePlay.Ins.isDeleteIron = true;
            DataManager.Ins.dataSaved.boosterBomb--;
            boosterDelete.ActiceBoosster(true, DataManager.Ins.dataSaved.boosterBomb);
            
        }
    }

    public void ShuffleScrew()
    {
        if (DataManager.Ins.dataSaved.boosterSuffer > 0)
        {
            LevelManager.Ins.currentLevel.ShufflerScrew();
            DataManager.Ins.dataSaved.boosterSuffer--;
            boosterShuffle.ActiceBoosster(true, DataManager.Ins.dataSaved.boosterSuffer);
        }
    }

    public void Undo()
    {
        if (DataManager.Ins.dataSaved.boosterUndo > 0)
        {
            LevelManager.Ins.currentLevel.Undo();
            DataManager.Ins.dataSaved.boosterUndo--;
            boosterUndo.ActiceBoosster(LevelManager.Ins.currentLevel.queueTile.numberScrew > 0, DataManager.Ins.dataSaved.boosterUndo);

        }
    }
    #endregion
}
