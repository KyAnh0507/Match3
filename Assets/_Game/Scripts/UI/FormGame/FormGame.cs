using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormGame : MonoBehaviour
{
    public bool isPauseGame;


    public PopupLose popupLose;
    public PopupWin popupWin;

    private void OnEnable()
    {
        
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
        LevelManager.Ins.currentLevel.queueTile.Add1Tile();
    }

    public void DeleteIron()
    {
        GamePlay.Ins.isDeleteIron = true;
    }

    public void ShuffleScrew()
    {
        LevelManager.Ins.currentLevel.ShufflerScrew();
    }

    public void Undo()
    {
        LevelManager.Ins.currentLevel.Undo();
    }
    #endregion
}
