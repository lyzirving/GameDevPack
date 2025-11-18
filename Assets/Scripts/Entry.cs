using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Entry : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("App entry");
        InputManager.Instance.Init();
    }
}
