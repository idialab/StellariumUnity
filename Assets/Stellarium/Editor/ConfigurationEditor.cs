using UnityEditor;

namespace Stellarium.Editor {

    [CustomEditor(typeof(Configuration))]
    [CanEditMultipleObjects]
    public class ConfigurationEditor : UnityEditor.Editor {

        public enum HostingType {
            LOCAL = 0,
            REMOTE = 1
        }

        HostingType hostingType = HostingType.LOCAL;
        SerializedProperty hostProp;
        SerializedProperty portProp;

        bool showHostProp = false;

        void OnEnable() {
            hostProp = serializedObject.FindProperty("host");
            portProp = serializedObject.FindProperty("port");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();
            hostingType = (HostingType)EditorGUILayout.EnumPopup("Hosting Type", hostingType);
            showHostProp = hostingType == HostingType.REMOTE;
            if(showHostProp) {
                hostProp.stringValue = EditorGUILayout.TextField("Host", hostProp.stringValue);
            } else {
                hostProp.stringValue = "localhost";
            }
            portProp.intValue = EditorGUILayout.IntField("Port", portProp.intValue);
            serializedObject.ApplyModifiedProperties();
        }

    }

}
