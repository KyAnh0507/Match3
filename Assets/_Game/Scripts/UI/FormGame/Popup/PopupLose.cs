using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupLose : MonoBehaviour
{
    public TextMeshPro LoseText;
    // Start is called before the first frame update
    void Start()
    {
        DataManager.Ins.dataSaved.currentWinstreak = 0;
    }
    
    public void Home()
    {
        UIManager.Ins.ChangeScene(Scene.Home);
        Close();
    }

    public void ReStart()
    {
        LevelManager.Ins.LoadLevel(DataManager.Ins.dataSaved.indexLevel);
        Close();
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
