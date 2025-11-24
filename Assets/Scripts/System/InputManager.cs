using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    private static InputManager m_Instance;
    public static InputManager instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new InputManager();
            }
            return m_Instance;
        }
    }

    private bool m_Enabled = false;

    public IA_Player actions { get; private set; }

    // Read only
    public bool isEnabled
    {
        get { return m_Enabled; }
    }

    public void Init()
    {
        actions = new IA_Player();
    }

    public void EnableInput()
    {
        actions?.Enable();
        m_Enabled = (actions != null);
    }

    public void DisableInput()
    {
        actions?.Disable();
        m_Enabled = false;
    }

    public void DisableAction(InputAction action)
    { 
        action?.Disable();
    }

    public void DisableActionForTime(InputAction action, float time)
    {
        CoroutineRunner.Run(DisableActionForTimeCoroutine(action, time));
    }

    private IEnumerator DisableActionForTimeCoroutine(InputAction action, float time)
    {
        action?.Disable();
        yield return new WaitForSeconds(time);
        action?.Enable();
    }
}
