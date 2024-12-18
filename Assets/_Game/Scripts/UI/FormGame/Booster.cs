using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Booster : MonoBehaviour
{
    public Button button;
    public Text noti;

    public void ActiceBoosster(bool b, int n)
    {
        button.interactable = b;
        if (n ==  0)
        {
            noti.text = "+";
            noti.fontSize = 65;
        }
        else
        {
            noti.text = n.ToString();
            noti.fontSize = 30;
        }

    }


}
