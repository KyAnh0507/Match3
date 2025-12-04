using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUI : CurrencyUI
{
    private void OnEnable()
    {
        DataManager.Ins.OnCoinChanged += OnCoinChanged;
        ChangeValue(DataManager.Ins.dataSaved.coin, 0f);
    }

    private void OnDisable()
    {
        if (DataManager.Ins != null)
        {
            DataManager.Ins.OnCoinChanged -= OnCoinChanged;
        }
    }

    private void OnCoinChanged(int value)
    {
        ChangeValue(value);
    }
}
