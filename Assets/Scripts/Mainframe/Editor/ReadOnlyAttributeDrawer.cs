using UnityEditor;
using UnityEngine;

namespace Scripts.Mainframe
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
        {

            if (prop.type == "CustomArray")
            {
                bool wasEnabled = GUI.enabled;
                bool fold = true;

                GUILayout.BeginVertical();
                {
                    fold = EditorGUILayout.Foldout(fold, prop.name);

                    if (fold)
                    {

                        GUI.enabled = false;

                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("Size:");
                            GUILayout.TextField(prop.arraySize.ToString());
                        }GUILayout.EndHorizontal();

                        for (int i = 0; i < prop.arraySize; i++)
                        {
                            string elementName = string.Format("Elemtent{0}:", i);
                            SerializedProperty p = prop.GetArrayElementAtIndex(i);

                            GUILayout.BeginHorizontal();
                            {
                                GUILayout.Label(elementName);
                                GUILayout.TextField(p.name);
                            }
                            GUILayout.EndHorizontal();
                        }

                    }
                }GUILayout.EndVertical();

                GUI.enabled = wasEnabled;
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
    }
}