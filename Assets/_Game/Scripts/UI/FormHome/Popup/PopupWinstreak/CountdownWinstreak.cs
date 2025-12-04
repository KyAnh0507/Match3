using System;
using System.Collections;
using System.Collections.Generic;
using TileCat3.Extensions;
using TMPro;
using UnityEngine;

public class CountdownWinstreak : MonoBehaviour
{
    public static string EndTimeKey = "EndTimeWinstreak";
    private const double EventDurationDays = 2d;

    public TMP_Text timeTmp;
    public TimeSpan time;

    private static readonly DateTime AnchorDate = new DateTime(2024, 12, 10, 0, 0, 0);
    private const int CycleLengthDays = 2;

    private int currentCycleIndex = -1;     // để detect khi sang vòng mới
    private DateTime blockStart;
    private DateTime blockEnd;

    public DateTime now, endTime;
    // Start is called before the first frame update
    /*void Start()
    {
        LoadOrCreateEndTime();
        *//*if (PlayerPrefs.HasKey(Endtime))
        {
            endOfDayTime = DateTime.Parse(PlayerPrefs.GetString(Endtime));
        }
        else
        {
            Initialize();
        }*//*
    }

    public void LoadOrCreateEndTime()
    {
        now = GameManager.Ins.Now();

        if (PlayerPrefs.HasKey(EndTimeKey))
        {
            // cố gắng parse, nếu lỗi thì tạo mới
            if (!DateTime.TryParse(PlayerPrefs.GetString(EndTimeKey), out endTime))
            {
                endTime = now.AddDays(EventDurationDays);
                SaveEndTime();
            }
            else
            {
                // nếu đã quá hạn (user off lâu), nhảy thẳng tới lượt kế tiếp
                AdvanceToNextValidCycle();
            }
        }
        else
        {
            // chưa có key -> tạo lượt đầu tiên
            endTime = now.AddDays(EventDurationDays);
            SaveEndTime();
        }
    }
    public void SaveEndTime()
    {
        PlayerPrefs.SetString(EndTimeKey, endTime.ToString("O"));
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Nhảy tới lượt 2-ngày tiếp theo sao cho endTime luôn > now.
    /// Dùng while để xử lý trường hợp user off nhiều ngày.
    /// </summary>
    public void AdvanceToNextValidCycle()
    {
        now = GameManager.Ins.Now();
        var duration = TimeSpan.FromDays(EventDurationDays);

        while (endTime <= now)
        {
            endTime = endTime.Add(duration);
        }

        SaveEndTime();
    }*/

    void Update()
    {
        DateTime now = GameManager.Ins.Now();

        int cycleIndex;
        CalculateCurrentBlock(now, out cycleIndex, out blockStart, out blockEnd);

        // Nếu vừa sang vòng mới
        if (cycleIndex != currentCycleIndex)
        {
            currentCycleIndex = cycleIndex;
            OnNewCycle(cycleIndex, blockStart, blockEnd);
        }

        // Đếm ngược tới cuối block hiện tại (blockEnd)
        TimeSpan remaining = blockEnd - now;
        if (remaining < TimeSpan.Zero)
            remaining = TimeSpan.Zero;

        if (timeTmp != null && timeTmp.gameObject.activeInHierarchy)
        {
            // Bạn có thể thay bằng hàm format cũ của bạn
            timeTmp.text = $"{remaining.Days:D2}:{remaining.Hours:D2}:{remaining.Minutes:D2}:{remaining.Seconds:D2}";
        }
    }

    /// <summary>
    /// Tính block (vòng 2 ngày) hiện tại dựa trên AnchorDate và now.
    /// </summary>
    void CalculateCurrentBlock(DateTime now, out int cycleIndex, out DateTime start, out DateTime end)
    {
        // Nếu hiện tại còn trước ngày mốc
        if (now < AnchorDate)
        {
            cycleIndex = 0;
            start = AnchorDate.Date;
            end = start.AddDays(CycleLengthDays); // 2 ngày: 10 & 11, kết thúc 00:00 ngày 12
            return;
        }

        // Số ngày đã trôi qua từ AnchorDate
        int daysSinceAnchor = (now.Date - AnchorDate.Date).Days;   // tính theo ngày

        // Mỗi block dài 2 ngày -> index block
        cycleIndex = daysSinceAnchor / CycleLengthDays;

        start = AnchorDate.Date.AddDays(cycleIndex * CycleLengthDays);
        end = start.AddDays(CycleLengthDays); // luôn là 2 ngày sau
    }

    /// <summary>
    /// Hàm này được gọi mỗi lần sang vòng winstreak mới (2 ngày mới).
    /// Bạn reset winstreak, set lại thưởng, v.v. ở đây.
    /// </summary>
    void OnNewCycle(int index, DateTime start, DateTime end)
    {
        // Ví dụ:
        // WinstreakManager.Instance.ResetForNewCycle(index);
        // Debug.Log($"New winstreak cycle {index}: {start} -> {end}");
    }
}
