using UnityEngine;
using UnityEditor;

namespace Scripts.MainframeReference.Debug
{
    [CustomEditor(typeof(TesterClass))]
    public class TestCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Excecute")) ((TesterClass)target).ExcecuteTask?.Invoke();            
        }
    }
}
