using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Entry : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("App entry");
        CoroutineRunner.Init();
        InputManager.instance.Init();
    }
}
