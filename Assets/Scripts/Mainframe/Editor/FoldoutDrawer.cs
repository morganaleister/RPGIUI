using UnityEditor;
using UnityEngine;

namespace Scripts.Mainframe
{
    public abstract class FoldoutDrawer : PropertyDrawer
    {
        protected float lineheight, totalwidth;
        protected bool foldout = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            lineheight = EditorGUIUtility.singleLineHeight;
            totalwidth = EditorGUIUtility.currentViewWidth;

            var r = new Rect(position.x, position.y, totalwidth, lineheight);
            foldout = EditorGUI.Foldout(position, foldout, GUIContent.none);

            if (foldout)
            {
                r = new Rect(position.x, position.y + lineheight, position.width, position.height);

                EditorGUI.PropertyField(r, property);
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (foldout)
                return base.GetPropertyHeight(property, label) + lineheight;
            else return lineheight;
        }
    }

}