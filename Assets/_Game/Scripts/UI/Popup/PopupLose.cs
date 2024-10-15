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
        
    }
    
    public void Home()
    {
        Close();
    }

    public void ReStart()
    {
        Close();

    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
