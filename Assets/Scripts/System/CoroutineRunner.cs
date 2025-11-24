using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner m_Instance;

    public static CoroutineRunner instance
    {
        get
        {
            CreateSingleton();
            return m_Instance;
        }
    }

    public static void Init()
    {
        CreateSingleton();
    }

    public static Coroutine Run(IEnumerator routine)
    {
        return instance.StartCoroutine(routine);
    }

    private static void CreateSingleton()
    {
        if (m_Instance == null)
        {
            GameObject runnerObj = new GameObject("CoroutineRunner");
            //runnerObj.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(runnerObj);
            m_Instance = runnerObj.AddComponent<CoroutineRunner>();
        }
    }
}
