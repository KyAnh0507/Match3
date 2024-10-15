using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormHome : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadGame()
    {
        UIManager.Ins.ChangeScene(Scene.Game);
    }
}
