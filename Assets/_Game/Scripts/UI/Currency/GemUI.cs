using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemUI : CurrencyUI
{
    private void OnEnable()
    {
        DataManager.Ins.OnGemChanged += OnGemChanged;
        ChangeValue(DataManager.Ins.dataSaved.coin, 0f);
    }

    private void OnDisable()
    {
        if (DataManager.Ins != null)
        {
            DataManager.Ins.OnGemChanged -= OnGemChanged;
        }
    }

    private void OnGemChanged(int value)
    {
        ChangeValue(value);
    }
}
