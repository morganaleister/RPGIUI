using UnityEditor;
using UnityEngine;

namespace Scripts.Mainframe.ScriptableObjects
{
    public class PhysiognomyDefinitionEditorWindow : EditorWindow
    {
        private static PhysiognomyDefinitionEditorWindow singleWindow;


        [SerializeField]
        private PhysiognomyDefinition _from, _to;

        public static void Open(PhysiognomyDefinition _target)
        {
            singleWindow =
                GetWindow<PhysiognomyDefinitionEditorWindow>(true, "Physiognomy EZ Populator", true);
            singleWindow.minSize = new Vector2(512, 256);
            singleWindow.maxSize = new Vector2(512, 256);

            singleWindow._to = _target;
        }

        public bool Clone()
        {
            try
            {
                PhysiognomyDefinition.Copy(_from, _to);
                return true;
            }
            catch (System.Exception)
            {
                throw;
                //return false;
            }
        }
        public bool Merge()
        {
            try
            {
                PhysiognomyDefinition.Merge(_from, _to);
                return true;
            }
            catch (System.Exception)
            {
                throw;
                //return false;
            }
        }

        public void OnGUI()
        {
            //Debug.Log("Window position is " + singleWindow.position.ToString());

            GUILayout.BeginVertical();
            {
                GUILayout.BeginHorizontal();
                {
                    DrawPhysiognomyDefRefField_from();
                    DrawPhysiognomyDefRefField_to();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Clone"))
                        Clone();
                    if (GUILayout.Button("Merge"))
                        Merge();

                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();
        }

        private void DrawPhysiognomyDefRefField_to()
        {
            GUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("To");


                SerializedObject slzdWindow = new SerializedObject(this);
                SerializedProperty slzdTo = slzdWindow.FindProperty("_to");

                EditorGUILayout.ObjectField(slzdTo, GUIContent.none);
                _to = (PhysiognomyDefinition)slzdTo.objectReferenceValue;
            }
            GUILayout.EndVertical();
        }
        private void DrawPhysiognomyDefRefField_from()
        {
            GUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("From");

                SerializedObject slzdWindow = new SerializedObject(this);
                SerializedProperty slzdFrom = slzdWindow.FindProperty("_from");

                EditorGUILayout.ObjectField(slzdFrom, GUIContent.none);
                _from = (PhysiognomyDefinition)slzdFrom.objectReferenceValue;

            }
            GUILayout.EndVertical();
        }

    }
}
