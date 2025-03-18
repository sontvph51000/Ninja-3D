using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    public float updateInterval = 0.5f; // Thời gian làm mới FPS (mỗi 0.5 giây)
    private float timeSinceLastUpdate = 0f;
    private int frameCount = 0;
    private float currentFPS;

    private GUIStyle guiStyle = new GUIStyle(); // Để tùy chỉnh giao diện văn bản

    void Start()
    {
        guiStyle.fontSize = 48; // Kích thước chữ
        guiStyle.normal.textColor = Color.white; // Màu chữ
    }

    void Update()
    {
        // Tính FPS
        timeSinceLastUpdate += Time.deltaTime;
        frameCount++;

        // Làm mới FPS mỗi updateInterval giây
        if (timeSinceLastUpdate >= updateInterval)
        {
            currentFPS = frameCount / timeSinceLastUpdate;
            timeSinceLastUpdate -= updateInterval;
            frameCount = 0;
        }
    }

    void OnGUI()
    {
        // Hiển thị FPS lên màn hình
        GUI.Label(new Rect(50, 50, 200, 50), "FPS: " + Mathf.Ceil(currentFPS), guiStyle);
    }
}
