using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputDetector : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CursorManager.Instance.IsCursorHide())
        {
            CursorManager.Instance.HideCursor();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame && CursorManager.Instance.IsCursorHide())
        {
            CursorManager.Instance.ShowCursor();
        }
    }
}
