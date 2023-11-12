using UnityEditor;
using UnityEngine;
using Unityroom.Internal;

namespace Unityroom.Editor
{
    using Editor = UnityEditor.Editor;

    [CustomEditor(typeof(UnityroomSettings))]
    public sealed class UnityroomSettingsEditor : Editor
    {
        readonly GUIContent label_hmacKey = new("HMAC Key");

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_hmacKey"), label_hmacKey);
            serializedObject.ApplyModifiedProperties();
        }
    }
}