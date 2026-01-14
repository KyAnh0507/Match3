using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayInDaylyChallenge : MonoBehaviour
{
    public Text numberDay;

    public GameObject noti;

    public Image image;
    public int order;
    public int indexLevelColorPencil;
    public void SetupDay(int day)
    {
        numberDay.text = day.ToString();
        if (day >= 1 && day <= 31)
        {
            indexLevelColorPencil = day - 1;
        }
    }

    public void Notify(bool b)
    {
        noti.SetActive(b);
    }
}
