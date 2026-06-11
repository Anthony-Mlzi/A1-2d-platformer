using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CanvasActive : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    

    public void unhide()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            canvasGroup.alpha = 1;
        }
    }
}