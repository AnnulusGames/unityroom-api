using UnityEditor;
using Unityroom.Internal;

namespace Unityroom.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(UnityroomSettings))]
    public sealed class UnityroomSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_hmacKey"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}