using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Unityroom.Internal;

namespace Unityroom.Editor
{
    using Editor = UnityEditor.Editor;

    public sealed class UnityroomSettingsProvider : SettingsProvider
    {
        const string MenuPath = "Project/Unityroom";
        const string SettingsAssetPath = "Assets/Unityroom/Resources/UnityroomSettings.asset";

        [SettingsProvider]
        public static SettingsProvider CreateSettingProvider()
        {
            return new UnityroomSettingsProvider(MenuPath, SettingsScope.Project, null);
        }

        public UnityroomSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords) : base(path, scopes, keywords)
        {
        }

        public override void OnGUI(string searchContext)
        {
            if (Settings == null)
            {
                if (GUILayout.Button("Create Settings Asset", GUILayout.Height(25f)))
                {
                    CreateSettingsAsset();
                    _editor = null;
                }
                else
                {
                    return;
                }
            }

            if (_editor == null)
            {
                Editor.CreateCachedEditor(_settings, null, ref _editor);
            }

            if (_editor != null && _editor.serializedObject != null) _editor.OnInspectorGUI();
        }

        static UnityroomSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    var guids = AssetDatabase.FindAssets($"t:{nameof(UnityroomSettings)}");
                    if (guids.Length == 0) return null;

                    var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    _settings = AssetDatabase.LoadAssetAtPath<UnityroomSettings>(path);
                }
                return _settings;
            }
        }
        static UnityroomSettings _settings;
        static Editor _editor;

        static void CreateSettingsAsset()
        {
            var folderPath = Path.GetDirectoryName(SettingsAssetPath);
            if (!string.IsNullOrEmpty(folderPath) && !Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var instance = ScriptableObject.CreateInstance<UnityroomSettings>();
            AssetDatabase.CreateAsset(instance, SettingsAssetPath);
            AssetDatabase.SaveAssets();
        }
    }
}