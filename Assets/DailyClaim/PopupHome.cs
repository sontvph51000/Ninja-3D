using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupHome : PopupBase
{
    public GameObject joyStick;
    public GameObject popupPlay;
    public void OpenDailyReward()
    {
        CanvasManager.instance.popupDailyClaim.Open();
    }

    public void PlayGame()
    {
        Close();
        joyStick.SetActive(true);
        popupPlay.SetActive(true);
    }


}
