using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSetting : MonoBehaviour
{
    public Transform tf;

    public GameObject musicOn;
    public GameObject musicOff;
    public GameObject soundOn;
    public GameObject soundOff;
    public GameObject vibrateOn;
    public GameObject vibrateOff;

    private void OnEnable()
    {
        musicOn.SetActive(DataManager.Ins.dataSaved.isMusicOn);
        musicOff.SetActive(!DataManager.Ins.dataSaved.isMusicOn);
        soundOn.SetActive(DataManager.Ins.dataSaved.isSoundOn);
        soundOff.SetActive(!DataManager.Ins.dataSaved.isSoundOn);
        vibrateOn.SetActive(DataManager.Ins.dataSaved.isVibrate);
        vibrateOff.SetActive(!DataManager.Ins.dataSaved.isVibrate);
    }


    public void ActiveMusic(bool b)
    {
        SoundManager.Ins.ChangeMusicSetting(b);
        musicOn.SetActive(DataManager.Ins.dataSaved.isMusicOn);
        musicOff.SetActive(!DataManager.Ins.dataSaved.isMusicOn);

        SoundManager.Ins.ChangeSound(SoundType.UI_CLICK);
        VibrateManager.Ins.TriggerVibrate();
    }

    public void ActiveSound(bool b)
    {
        SoundManager.Ins.ChangeSoundSetting(b);
        soundOn.SetActive(DataManager.Ins.dataSaved.isSoundOn);
        soundOff.SetActive(!DataManager.Ins.dataSaved.isSoundOn);

        SoundManager.Ins.ChangeSound(SoundType.UI_CLICK);
        VibrateManager.Ins.TriggerVibrate();
    }

    public void ActiveVibrate(bool b)
    {
        DataManager.Ins.dataSaved.isVibrate = b;
        vibrateOn.SetActive(DataManager.Ins.dataSaved.isVibrate);
        vibrateOff.SetActive(!DataManager.Ins.dataSaved.isVibrate);

        SoundManager.Ins.ChangeSound(SoundType.UI_CLICK);
        VibrateManager.Ins.TriggerVibrate();
    }
    public void Close()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
            if (UIManager.Ins.formGame != null)
            {
                UIManager.Ins.formGame.ResumeGame();
            }
        });
    }
}
