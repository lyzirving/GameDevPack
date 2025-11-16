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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InputManager.Instance.EnableInput();
    }

    public bool IsCursorHide()
    {
        return Cursor.visible == false;
    }
}
