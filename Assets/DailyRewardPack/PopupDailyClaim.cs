using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupDailyClaim : PopupBase
{
    public List<DayItem> dayItems = new List<DayItem>();

    private void Awake()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(KeyDataDaiLy.timeSave.ToString())))
        {
            DateTime dateTimeFirstLogin = new DateTime(2020, 10, 10, 10, 0, 0, DateTimeKind.Utc);
            PlayerPrefs.SetString(KeyDataDaiLy.timeSave.ToString(), dateTimeFirstLogin.ToString());
            PlayerPrefs.SetInt(KeyDataDaiLy.dayClaim.ToString(), 0);
        }
    }
    
    public override void Open()
    {
        base.Open();
        DaySetup();
    }

    public void DaySetup()
    {
        DateTime oldTime = DateTime.Parse(PlayerPrefs.GetString(KeyDataDaiLy.timeSave.ToString()));
        TimeSpan distance = DateTime.Now - oldTime;
        int currentDay = PlayerPrefs.GetInt(KeyDataDaiLy.dayClaim.ToString());
        foreach (DayItem dayItem in dayItems)
        {
            int x = dayItems.IndexOf(dayItem);
            dayItem.indexx = x;
            dayItem.SetData();

            if(x < currentDay)
            {
                dayItem.Claimed();
            }

            if(distance.TotalHours >= 24 && x == currentDay) 
            {
                dayItem.FocusDay();
            }

            if(distance.TotalHours < 24 && x == currentDay)
            {
                dayItem.NextDay(true);
            }
            if(distance.TotalHours > 24 && x == currentDay + 1)
            {
                dayItem.NextDay(false);
            }

            if(distance.TotalHours < 24 && x == currentDay + 1)
            {
                dayItem.AfterTomorrow();
            }
            if( x > currentDay + 1)
            {
                dayItem.AfterTomorrow();
            }
        }
    }

}

public enum KeyDataDaiLy
{
    timeSave,
    dayClaim
}
