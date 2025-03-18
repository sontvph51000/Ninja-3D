using UnityEngine;

public class PopupBase : MonoBehaviour
{

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
