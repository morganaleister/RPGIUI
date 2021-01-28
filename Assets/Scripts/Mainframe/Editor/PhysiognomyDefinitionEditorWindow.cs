using UnityEditor;
using UnityEngine;

namespace Scripts.Mainframe.ScriptableObjects
{
    public class PhysiognomyDefinitionEditorWindow : EditorWindow
    {
        private static PhysiognomyDefinitionEditorWindow singleWindow;

        SerializedObject slzdWindow;
        SerializedProperty slzdTo, 
            slzdBP, slzdBPName, slzdBPPluralName, 
            slzdBPCrit, slzdBPGauge, slzdBPSubParts;

        private bool addPartsButton, _defaultBPfoldout;
        private int addPartsNum;
        
        private string addPartsBtnText;


        [SerializeField]
        private PhysiognomyDefinition _from, _to;
        [SerializeField]
        private BodyPart _defaultPart = new BodyPart();

        public static void Open(PhysiognomyDefinition _target)
        {
            singleWindow =
                GetWindow<PhysiognomyDefinitionEditorWindow>(true, "Physiognomy EZ Populator", true);
            singleWindow.minSize = new Vector2(768, 512);
            singleWindow.maxSize = new Vector2(768, 512);

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
            Initialize();

            //Debug.Log("Window position is " + singleWindow.position.ToString());
            GUILayout.BeginVertical();
            {
                DrawPhysiognomyDefRefField_from();
                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Clone"))
                        Clone();

                    if (GUILayout.Button("Merge"))
                        Merge();
                }
                GUILayout.EndHorizontal();

                DrawPhysiognomyDefRefField_to();

                DrawPartSelector();

            }
            GUILayout.EndVertical();
        }

        private void Initialize()
        {
            slzdWindow = new SerializedObject(this);

            slzdTo = slzdWindow.FindProperty("_to");

            slzdBP = slzdWindow.FindProperty("_defaultPart");
            slzdBPName = slzdBP.FindPropertyRelative("_partName");
            slzdBPPluralName = slzdBP.FindPropertyRelative("_pluralName");
            slzdBPCrit = slzdBP.FindPropertyRelative("_critical");
            slzdBPGauge = slzdBP.FindPropertyRelative("_hp");
            slzdBPSubParts = slzdBP.FindPropertyRelative("_sharedSubParts");

        }

        private void DrawPartSelector()
        {
            slzdBP.isExpanded = true;
            _defaultBPfoldout = 
                EditorGUILayout.BeginFoldoutHeaderGroup
                (_defaultBPfoldout, SetAddPartsBtnText());

            if (_defaultBPfoldout)
            {
                slzdBPName.stringValue = 
                    EditorGUILayout.TextField("Part Name", slzdBPName.stringValue);
                slzdBPPluralName.stringValue = 
                    EditorGUILayout.TextField("Plural Name", slzdBPPluralName.stringValue);
                slzdBPCrit.boolValue = 
                    EditorGUILayout.Toggle("Critical", slzdBPCrit.boolValue);
                
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

        }

        private void DrawAddX()
        {
            GUILayout.BeginHorizontal();
            {
                SetAddPartsBtnText();

                var fs = string.Format("Add {0} {1}", addPartsNum, addPartsBtnText);
                var gc = new GUIContent("x");

                addPartsNum = EditorGUILayout.IntField(gc, addPartsNum);
                addPartsButton = GUILayout.Button
                    (fs, GUILayout.MaxWidth(EditorGUIUtility.labelWidth));
                if (addPartsButton)
                    AddPartsTo();
            }
            GUILayout.EndHorizontal();
        }

        private string SetAddPartsBtnText()
        {
            if (addPartsNum <= 1)
                addPartsBtnText = slzdBPName.stringValue;
            else
                addPartsBtnText = slzdBPPluralName.stringValue;

            return addPartsBtnText;
        }

        private void AddPartsTo()
        {
            throw new System.NotImplementedException();
        }

        private void DrawPhysiognomyDefRefField_to()
        {
            GUILayout.BeginVertical();
            {
                GUILayout.BeginHorizontal();
                {
                    DrawAddX();
                    EditorGUILayout.LabelField("To");
                   
                }
                GUILayout.EndHorizontal();                

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
