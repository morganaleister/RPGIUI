using UnityEditor;
using UnityEngine;

namespace Scripts.Mainframe
{
    [CustomEditor(typeof(BaseButton))]
    public class BaseButtonCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

    }
}
