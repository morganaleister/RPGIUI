using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Scripts.Mainframe.ScriptableObjects
{
    public class AssetHandler
    {
        [OnOpenAsset()]
        public static bool OpenEditor(int instanceId, int line)
        {
            PhysiognomyDefinition obj =
                EditorUtility.InstanceIDToObject(instanceId) as PhysiognomyDefinition;

            if (obj != null)
            {
                PhysiognomyDefinitionEditorWindow.Open(obj);
                return true;
            }

            return false;
        }
    }

    //[CustomEditor(typeof(PhysiognomyDefinition))]
    //public class PhysiognomyDefinitionEditor : Editor
    //{
    //    public override void OnInspectorGUI()
    //    {
    //        Event e = Event.current;

    //        switch (e.type)
    //        {
    //            case EventType.MouseDown:
    //                //Debug.Log("mouse down");
    //                break;
    //            case EventType.MouseEnterWindow:
    //                Debug.Log("mouse left a window");
    //                break;
    //            case EventType.MouseLeaveWindow:
    //                Debug.Log("mouse entered a window");
    //                break;
    //            default:
    //                break;
    //        }

    //        base.OnInspectorGUI();

    //        var click = GUILayout.Button("Populate");

    //        if (click)
    //        {
    //            PhysiognomyDefinitionEditorWindow.Open((PhysiognomyDefinition)target);
    //        }
    //    }

    //}
}
