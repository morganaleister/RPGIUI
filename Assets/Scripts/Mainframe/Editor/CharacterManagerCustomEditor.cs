using System;
using UnityEditor;
using UnityEngine;

namespace Scripts.MainframeReference
{
    [CustomEditor(typeof(CharacterManager))]
    public class CharacterManagerCustomEditor : Editor
    {
        bool fs_createButtons = true;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginVertical();
            {
                
                fs_createButtons = EditorGUILayout.Foldout(fs_createButtons, "Create Buttons", true);
                if (fs_createButtons)
                {
                    DrawCreateCharacterControl();
                }
                
            }GUILayout.EndVertical();
        }

        private void DrawCreateCharacterControl()
        {
            CharacterManager m = target as CharacterManager;
            string[] charTypesNames = Enum.GetNames(typeof(Constraint));
            
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Character Type");
                CharacterManager._constraint =
                    (Constraint)EditorGUILayout.Popup
                        ("", (int)CharacterManager._constraint, charTypesNames);
                if (GUILayout.Button("Create"))
                    CharacterManager.CreateCharacter();
            }
            GUILayout.EndHorizontal();            
        }
    }
}