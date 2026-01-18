using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PopupWin : MonoBehaviour
{
    public SpinWin spinWin;
    public ParticleImage vfxConfetti;

    public ParticleImage coinPI;
    public Transform coinIcon;
    public TMP_Text atext;
    public Tween atween;

    private void OnEnable()
    {
        DataManager.Ins.dataSaved.currentWinstreak++;
        if (DataManager.Ins.dataSaved.currentWinstreak > DataManager.Ins.dataSaved.maxWinstreak)
        {
            DataManager.Ins.dataSaved.maxWinstreak = DataManager.Ins.dataSaved.currentWinstreak;
        }

        spinWin.arrow.rotation = Quaternion.Euler(new Vector3(0, 0, 89f));
        atween = spinWin.arrow.DORotate(new Vector3(0, 0, -89), 1.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).OnUpdate(() =>
        {
            float angle = spinWin.arrow.eulerAngles.z;
            angle = angle > 180 ? angle - 360 : angle;
            if (angle < -54 || angle > 54)
            {
                atext.text = "+" + 20 + "";
            }
            else if (angle < -18 || angle > 18)
            {
                atext.text = "+" + 30 + "";
            }
            else /*if (spinWin.arrow.rotation.z < -40.5 || spinWin.arrow.rotation.z > 43.5)*/
            {
                atext.text = "+" + 50 + "";
            }
        });


        vfxConfetti.Play();
    }
    public void Home()
    {
        SoundManager.Ins.ChangeSound(SoundType.UI_CLICK);
        if (atween != null && atween.IsActive() && atween.IsPlaying())
        {
            atween.Kill(); // Hoặc .Kill() nếu muốn dừng hoàn toàn và không bao giờ dùng lại tween này
        }
        int reward = int.Parse(atext.text.Substring(1));
        DataManager.Ins.ChangeCoin(reward);
        UIManager.Ins.formGame.SetOverrideCoin(true);
        UIManager.Ins.SetActiveBlock(true);
        coinPI.transform.position = coinIcon.position;
        coinPI.attractorTarget = UIManager.Ins.formGame.coinUI.IconTf;
        coinPI.Play();

        coinPI.onLastParticleFinish.AddListener(() =>
        {
            UIManager.Ins.formGame.SetOverrideCoin(false);
            UIManager.Ins.SetActiveBlock(false);
            //LevelManager.Ins.LoadLevel(DataManager.Ins.dataSaved.indexLevel);
            UIManager.Ins.ChangeScene(Scene.Home);
            Close();
        });
    }

    public void NextLevel()
    {
        if (DataManager.Ins.dataSaved.indexLevel >= LevelManager.Ins.levelGameModels.Count)
        {
            DataManager.Ins.dataSaved.indexLevel = Random.Range(0, LevelManager.Ins.levelGameModels.Count);
        }

        SoundManager.Ins.ChangeSound(SoundType.UI_CLICK);
        if (atween != null && atween.IsActive() && atween.IsPlaying())
        {
            atween.Kill(); // Hoặc .Kill() nếu muốn dừng hoàn toàn và không bao giờ dùng lại tween này
        }
        int reward = int.Parse(atext.text.Substring(1));
        DataManager.Ins.ChangeCoin(reward);
        UIManager.Ins.formGame.SetOverrideCoin(true);
        UIManager.Ins.SetActiveBlock(true);
        coinPI.transform.position = coinIcon.position;
        coinPI.attractorTarget = UIManager.Ins.formGame.coinUI.IconTf;
        coinPI.Play();

        coinPI.onLastParticleFinish.AddListener(() =>
        {
            UIManager.Ins.formGame.SetOverrideCoin(false);
            UIManager.Ins.SetActiveBlock(false);
            //LevelManager.Ins.LoadLevel(DataManager.Ins.dataSaved.indexLevel);
            UIManager.Ins.ChangeScene(Scene.Game);
            Close();
        });
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
