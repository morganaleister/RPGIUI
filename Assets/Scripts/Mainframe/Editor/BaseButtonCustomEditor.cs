using UnityEditor;

namespace Scripts.MainframeReference
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
