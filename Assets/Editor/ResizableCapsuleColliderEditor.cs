using UnityEditor;

[CustomEditor(typeof(ResizableCapsuleCollider))]
public class ResizeableCapsuleColliderEditor : Editor
{
    private SerializedProperty m_CapsuleColliderData;
    private SerializedProperty m_LocalCenter;
    private SerializedProperty m_VerticalExtents;

    private SerializedProperty m_DefaultColliderData;
    private SerializedProperty m_Height;
    private SerializedProperty m_CenterY;
    private SerializedProperty m_Radius;

    private SerializedProperty m_SlopeData;
    private SerializedProperty m_StepHeightPercentage;
    private SerializedProperty m_FloatRayDistance;
    private SerializedProperty m_StepReachForce;

    private void OnEnable()
    {
        m_CapsuleColliderData = serializedObject.FindProperty("m_CapsuleColliderData");
        if (m_CapsuleColliderData != null)
        {
            m_LocalCenter = m_CapsuleColliderData.FindPropertyRelative("centerInLocalSpace");
            m_VerticalExtents = m_CapsuleColliderData.FindPropertyRelative("verticalExtents");
        }
        else
        {
            throw new System.Exception("fail to get prop m_CapsuleColliderData");
        }

        m_DefaultColliderData = serializedObject.FindProperty("m_DefaultColliderData");
        if (m_DefaultColliderData != null)
        {
            m_Height = m_DefaultColliderData.FindPropertyRelative("height");
            m_CenterY = m_DefaultColliderData.FindPropertyRelative("centerY");
            m_Radius = m_DefaultColliderData.FindPropertyRelative("radius");
        }
        else 
        {
            throw new System.Exception("fail to get prop m_DefaultColliderData");
        }

        m_SlopeData = serializedObject.FindProperty("m_SlopeData");
        if (m_SlopeData != null)
        {
            m_StepHeightPercentage = m_SlopeData.FindPropertyRelative("stepHeightPercentage");
            m_FloatRayDistance = m_SlopeData.FindPropertyRelative("floatRayDistance");
            m_StepReachForce = m_SlopeData.FindPropertyRelative("stepReachForce");
        }
        else
        {
            throw new System.Exception("fail to get prop m_SlopeData");
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("CapusuleColliderData", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("center", m_LocalCenter.vector3Value.ToString(), EditorStyles.numberField);
        EditorGUILayout.LabelField("extents", m_VerticalExtents.vector3Value.ToString(), EditorStyles.numberField);

        EditorGUILayout.LabelField("DefaultColliderData", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("height", m_Height.floatValue.ToString(), EditorStyles.numberField);
        EditorGUILayout.LabelField("centerY", m_CenterY.floatValue.ToString(), EditorStyles.numberField);
        EditorGUILayout.LabelField("radius", m_Radius.floatValue.ToString(), EditorStyles.numberField);

        EditorGUILayout.LabelField("SlopeData", EditorStyles.boldLabel);
        EditorGUILayout.Slider(m_StepHeightPercentage, 0f, 1f, "stepHeightPercentage");
        EditorGUILayout.Slider(m_FloatRayDistance, 0f, 5f, "floatRayDistance");
        EditorGUILayout.Slider(m_StepReachForce, 0f, 50f, "stepReachForce");

        serializedObject.ApplyModifiedProperties();
    }
}
