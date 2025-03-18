using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DayItem : MonoBehaviour
{
    public int indexx;
    public Image focus;
    public Image iconReward;
    public TextMeshProUGUI text_Day;
    public TextMeshProUGUI text_Amount;
    public Image gray_Img;
    public GameObject clear;
    public Button button;
    public GameObject tomorrow;
    public DayState dayState;

    public void SetData()
    {
        text_Day.text = "Day "+ (indexx +1);
    }

    public void Claimed()
    {
        clear.SetActive(true);
        gray_Img.gameObject.SetActive(true);
        button.interactable = false;
        focus.gameObject.SetActive(false);
        tomorrow.SetActive(false);
        dayState = DayState.Claimed;
    }

    public void FocusDay()
    {
        clear.SetActive(false);
        gray_Img.gameObject.SetActive(false);
        focus.gameObject.SetActive(true);
        button.interactable = true;
        tomorrow.SetActive(false);
        dayState = DayState.FocusDay;
    }

    public void NextDay(bool isNextdayFocus)
    {
        clear.SetActive(false);
        gray_Img.gameObject.SetActive(false);
        focus.gameObject.SetActive(isNextdayFocus);
        button.interactable = false;
        tomorrow.SetActive(true);
        dayState = DayState.NextDay;
    }

    public void AfterTomorrow()
    {
        clear.SetActive(false);
        gray_Img.gameObject.SetActive(false);
        focus.gameObject.SetActive(false);
        button.interactable = false;
        tomorrow.SetActive(false);
        dayState = DayState.AfterTomorrow;
    }

    public void Claim()
    {
        string timeSevenHour = SetTimeToFixedHour(DateTime.UtcNow, 7).ToString();
        PlayerPrefs.SetString(KeyDataDaiLy.timeSave.ToString(), timeSevenHour);
        PlayerPrefs.SetInt(KeyDataDaiLy.dayClaim.ToString(), PlayerPrefs.GetInt(KeyDataDaiLy.dayClaim.ToString()) + 1);
        if(PlayerPrefs.GetInt(KeyDataDaiLy.dayClaim.ToString()) == 28)
        {
             PlayerPrefs.SetInt(KeyDataDaiLy.dayClaim.ToString(),0);
        }
        
        Debug.Log(transform.parent.parent.gameObject.name);
        transform.parent.parent.gameObject.GetComponent<PopupDailyClaim>().DaySetup();
    }

    public DateTime SetTimeToFixedHour(DateTime dateTime, int hour)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, 0, 0, DateTimeKind.Utc);
    }
}
public enum DayState
{
    Claimed,
    FocusDay,
    NextDay,
    AfterTomorrow
}
