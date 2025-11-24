using UnityEngine;

public class CursorManager
{
    private static CursorManager m_Instance;
    
    public static CursorManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new CursorManager();
            }
            return m_Instance;
        }
    }

    public void HideCursor()
    {
        Debug.Log("HideCursor");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InputManager.instance.EnableInput();
    }

    public void ShowCursor()
    {
        Debug.Log("ShowCursor");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        InputManager.instance.DisableInput();
    }

    public bool IsCursorHide()
    {
        return Cursor.visible == false;
    }
}
