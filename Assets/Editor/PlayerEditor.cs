using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var comp = (Player)target;

        if (comp.config == null)
        {
            EditorGUILayout.HelpBox("PlayerConfig hasn't been set yet!", MessageType.Error);
        }

        base.OnInspectorGUI();
    }
}
