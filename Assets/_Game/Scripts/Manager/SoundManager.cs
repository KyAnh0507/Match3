using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgAudio;
    public AudioSource fxAudio;

    public AudioClip bgClip;

    public AudioClip screwInsertClip;
    public AudioClip uiClickClip;
    public AudioClip popupClip;
    public AudioClip gameWinClip;
    public AudioClip metalClip1;
    public AudioClip metalClip2;

    private void OnEnable()
    {
        bgAudio.mute = !DataManager.Ins.dataSaved.isMusicOn;
        fxAudio.mute = !DataManager.Ins.dataSaved.isSoundOn;
    }

    public void ChangeMusicSetting(bool b)
    {
        DataManager.Ins.dataSaved.isMusicOn = b;
        bgAudio.mute = !b;
    }

    public void ChangeSoundSetting(bool b)
    {
        DataManager.Ins.dataSaved.isSoundOn = b;
        fxAudio.mute = !DataManager.Ins.dataSaved.isSoundOn;
    }

    public void ChangeSound(SoundType soundType)
    {
        if (DataManager.Ins.dataSaved.isSoundOn)
        {
            switch (soundType)
            {
                case SoundType.SCREW_INSERT:
                    fxAudio.clip = screwInsertClip;
                    fxAudio.Play();
                    break;
                case SoundType.UI_CLICK:
                    fxAudio.clip = uiClickClip;
                    fxAudio.Play();
                    break;
                case SoundType.POPUP_CLICK:
                    fxAudio.clip = popupClip;
                    fxAudio.Play();
                    break;
                case SoundType.GAME_WIN:
                    fxAudio.clip = gameWinClip;
                    fxAudio.Play();
                    break;
                case SoundType.METAL_1:
                    fxAudio.clip = metalClip1;
                    fxAudio.Play();
                    break;
                case SoundType.METAL_2:
                    fxAudio.clip = metalClip2;
                    fxAudio.Play();
                    break;
                default:
                    break;
            }
        }
    }
}

public enum SoundType{
    SCREW_INSERT,
    UI_CLICK,
    POPUP_CLICK,
    GAME_WIN,
    METAL_1,
    METAL_2
}