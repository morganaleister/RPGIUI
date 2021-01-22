using UnityEngine;
using UnityEditor;

namespace Scripts.Mainframe.Debuggering
{
    [CustomEditor(typeof(TesterClass))]
    public class TesterClassCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Excecute")) ((TesterClass)target).ExcecuteTask?.Invoke();            
        }
    }
}
