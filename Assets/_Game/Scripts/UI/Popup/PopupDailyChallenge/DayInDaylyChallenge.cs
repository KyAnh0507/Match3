using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayInDaylyChallenge : MonoBehaviour
{
    public Text numberDay;

    public GameObject noti;

    public void SetupDay(int day)
    {
        numberDay.text = day.ToString();
    }

    public void Notify(bool b)
    {
        noti.SetActive(b);
    }

    public void SelectDay()
    {

    }
}
