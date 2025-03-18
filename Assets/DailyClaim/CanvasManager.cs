using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    public PopupDailyClaim popupDailyClaim;

    void Awake()
    {
        instance = this;
    }

}
