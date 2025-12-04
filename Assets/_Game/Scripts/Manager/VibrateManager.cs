using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateManager : Singleton<VibrateManager>
{
    public void TriggerVibrate()
    {
        if (DataManager.Ins.dataSaved.isVibrate)
        {
            Handheld.Vibrate();
        }
    }
}
