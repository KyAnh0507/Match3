using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownMonthly : MonoBehaviour
{
    public TMP_Text timeTmp;
    // Neo mốc (tháng bắt đầu). Có thể chỉnh tuỳ ý.
    // Nếu bạn muốn luôn tính theo tháng hiện tại của hệ thống thì cũng OK, nhưng để neo giúp cycleIndex ổn định.
    private static readonly DateTime AnchorMonth = new DateTime(2024, 12, 1, 0, 0, 0);

    //private int currentCycleIndex = -1;
    private DateTime blockStart;
    private DateTime blockEnd;

    void Update()
    {
        DateTime now = GameManager.Ins.Now();

        int cycleIndex;
        CalculateCurrentMonthlyBlock(now, out cycleIndex, out blockStart, out blockEnd);

        // Đếm ngược tới cuối tháng (blockEnd)
        TimeSpan remaining = blockEnd - now;
        if (remaining < TimeSpan.Zero)
            remaining = TimeSpan.Zero;

        if (timeTmp != null && timeTmp.gameObject.activeInHierarchy)
        {
            // Hiển thị dạng DD:HH:MM:SS
            timeTmp.text = $"{remaining.Days}D{remaining.Hours:D2}H";
        }
    }

    /// <summary>
    /// Tính block theo THÁNG: start = 00:00 ngày 1 của tháng hiện tại,
    /// end = 00:00 ngày 1 của tháng kế tiếp.
    /// cycleIndex tăng mỗi tháng kể từ AnchorMonth.
    /// </summary>
    void CalculateCurrentMonthlyBlock(DateTime now, out int cycleIndex, out DateTime start, out DateTime end)
    {
        // Chuẩn hoá về đầu tháng hiện tại
        DateTime currentMonthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0);

        // Nếu now trước mốc neo
        if (currentMonthStart < new DateTime(AnchorMonth.Year, AnchorMonth.Month, 1, 0, 0, 0))
        {
            cycleIndex = 0;
            start = new DateTime(AnchorMonth.Year, AnchorMonth.Month, 1, 0, 0, 0);
            end = start.AddMonths(1);
            return;
        }

        // Tính số tháng chênh lệch giữa AnchorMonth và currentMonthStart
        cycleIndex = (currentMonthStart.Year - AnchorMonth.Year) * 12 + (currentMonthStart.Month - AnchorMonth.Month);

        start = currentMonthStart;
        end = currentMonthStart.AddMonths(1);
    }
}
