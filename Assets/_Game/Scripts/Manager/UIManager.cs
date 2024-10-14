using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public FormGame formGame;
    public FormLoading formLoading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(Scene sceneName)
    {

    }
}

public enum Scene { Loading, Game}
