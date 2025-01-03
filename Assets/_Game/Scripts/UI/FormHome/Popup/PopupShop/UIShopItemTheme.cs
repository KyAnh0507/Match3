using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopItemTheme : MonoBehaviour
{
    public RewardType currencyType;

    public int price;
    public int nTheme;

    public GameObject locked;
    public GameObject unlock;

    public void Setup()
    {
        if (DataManager.Ins.dataSaved.statusTheme[nTheme])
        {
            locked.gameObject.SetActive(false);
            unlock.gameObject.SetActive(true);
        }
        else
        {
            locked.gameObject.SetActive(true);
            unlock.gameObject.SetActive(false);
        }
    }

    public void Buy()
    {
        locked.gameObject.SetActive(false);
        unlock.gameObject.SetActive(false);
    }
}
