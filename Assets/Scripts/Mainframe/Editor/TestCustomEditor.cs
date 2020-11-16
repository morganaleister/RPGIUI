using UnityEngine;
using UnityEditor;

namespace Scripts.MainframeReference.CustomDebugger
{
    [CustomEditor(typeof(Test))]
    public class TestCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Test!")) ((Test)target).onTest?.Invoke();            
        }
    }
}
