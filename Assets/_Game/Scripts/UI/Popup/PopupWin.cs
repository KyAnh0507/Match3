using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWin : MonoBehaviour
{
    public void Home()
    {
        UIManager.Ins.ChangeScene(Scene.Home);
        Close();
    }

    public void NextLevel()
    {
        if (DataManager.Ins.dataSaved.indexLevel >= LevelManager.Ins.levels.Count)
        {
            DataManager.Ins.dataSaved.indexLevel = Random.Range(0, LevelManager.Ins.levels.Count);
        }
        LevelManager.Ins.LoadLevel();
        Close();
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
