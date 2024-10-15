using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWin : MonoBehaviour
{
    public void Home()
    {
        Close();
    }

    public void NextLevel()
    {
        Close();
        LevelManager.Ins.LoadLevel();
        DataManager.Ins.dataSaved.level++;
        if (DataManager.Ins.dataSaved.level >= LevelManager.Ins.levels.Count)
        {
            int newIndex = Random.Range(0, LevelManager.Ins.levels.Count);
            if (newIndex == DataManager.Ins.dataSaved.indexLevel)
            {
                DataManager.Ins.dataSaved.indexLevel++;
            }
            else
            {
                DataManager.Ins.dataSaved.indexLevel = newIndex;
            }
        }
        else
        {
            DataManager.Ins.dataSaved.indexLevel = DataManager.Ins.dataSaved.level;
        }
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
