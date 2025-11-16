using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CursorManager.Instance.IsCursorHide())
        { 
            CursorManager.Instance.HideCursor();
        }
    }
}
