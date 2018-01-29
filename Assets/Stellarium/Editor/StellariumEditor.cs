using UnityEditor;

namespace Stellarium.Editor {
    [CustomEditor(typeof(StellariumServer))]
    public class StellariumManagerEditor : UnityEditor.Editor {

        SerializedProperty configurationProp;

        void OnEnable() {
            configurationProp = serializedObject.FindProperty("configuration");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            configurationProp.objectReferenceValue = EditorGUILayout.ObjectField("Configuration",configurationProp.objectReferenceValue, typeof(Configuration),false);
            if(configurationProp.objectReferenceValue != null) {
                EditorGUILayout.LabelField("Host", (configurationProp.objectReferenceValue as Configuration).host);
                EditorGUILayout.LabelField("Port", (configurationProp.objectReferenceValue as Configuration).port.ToString());
            }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
