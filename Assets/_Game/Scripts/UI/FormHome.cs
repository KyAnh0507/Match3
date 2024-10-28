using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormHome : MonoBehaviour
{
    public GameObject popupDailyReward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadGame()
    {
        UIManager.Ins.ChangeScene(Scene.Game);
    }

    public void OpenPopupDailyReward()
    {
        popupDailyReward.SetActive(true);
    }
}
