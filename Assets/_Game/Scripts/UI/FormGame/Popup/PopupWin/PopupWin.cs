using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWin : MonoBehaviour
{
    public SpinWin spinWin;
    public ParticleImage vfxConfetti;

    private void OnEnable()
    {
        DataManager.Ins.dataSaved.currentWinstreak++;
        if (DataManager.Ins.dataSaved.currentWinstreak > DataManager.Ins.dataSaved.maxWinstreak)
        {
            DataManager.Ins.dataSaved.maxWinstreak = DataManager.Ins.dataSaved.currentWinstreak;
        }
        vfxConfetti.Play();
    }
    public void Home()
    {
        UIManager.Ins.ChangeScene(Scene.Home);
        Close();
    }

    public void NextLevel()
    {
        if (DataManager.Ins.dataSaved.indexLevel >= LevelManager.Ins.levelGameModels.Count)
        {
            DataManager.Ins.dataSaved.indexLevel = Random.Range(0, LevelManager.Ins.levelGameModels.Count);
        }
        LevelManager.Ins.LoadLevel(DataManager.Ins.dataSaved.indexLevel);
        Close();
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
