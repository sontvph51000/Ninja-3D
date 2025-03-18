using UnityEngine;

public class SetTargetFrameRate : MonoBehaviour
{
    void Start()
    {
        // Đặt target frame rate là 60 FPS
        Application.targetFrameRate = 60;
    }
}
