using UnityEditor;
using UnityEngine;

namespace Scripts.MainframeReference
{
    [CustomPropertyDrawer(typeof(Gauge))]
    public class GaugeDrawer : PropertyDrawer
    {
        SerializedProperty typeSlzdProp, currentSlzdProp, maxSlzdProp, underflowSlzdProp, overflowSlzdProp;
        string typeValue;
        float currentValue, maxValue;
        bool underflowValue, overflowValue;
        bool foldout = true;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect newPos = new Rect(position.x, position.y + 4, position.width, position.height);

            EditorGUI.BeginProperty(position, label, property);            

            EditorGUI.DrawRect(newPos, Color.Lerp(Color.white, Color.black, 0.8f));

            newPos = new Rect(position.x, newPos.y, 300, 100);            

            newPos = new Rect(position.x + 10, newPos.y, 322, 15);

            foldout = EditorGUI.BeginFoldoutHeaderGroup(newPos, foldout, GUIContent.none);

            newPos = DrawTitleLabel(property, newPos);

            EditorGUI.EndFoldoutHeaderGroup();

            if (foldout)
            {

                newPos = DrawTypeTextField(newPos);

                newPos = DrawUnderOverFlows(property, newPos);

                newPos = DrawCurrentMax(property, newPos);

                newPos = new Rect(newPos.x, newPos.y + 22, newPos.width, 18);
                DrawProgressBar(newPos);

            }
            else
            {
                newPos = new Rect(newPos.x, newPos.y + 25, newPos.width, 18);
                DrawProgressBar(newPos);
            }

            EditorGUI.EndProperty();
        }

        private void DrawProgressBar(Rect position)
        {
            Gauge value = UpdateCurrent(currentSlzdProp.floatValue);
            string fraction = string.Format("{0} / {1}", value.Fraction.x, value.Fraction.y);


            EditorGUI.ProgressBar(position, value.Normalized, fraction);
        }

        private Rect DrawTitleLabel(SerializedProperty property, Rect position)
        {

            typeSlzdProp = property.FindPropertyRelative("_type");
            typeValue = typeSlzdProp.stringValue.Trim();

            string titleText = string.Format("{0} ({1})", typeValue, property.type);
            GUIStyle titleStyle = new GUIStyle();
            {

                titleStyle.fontSize = 12;
                titleStyle.fontStyle = FontStyle.Bold;
                titleStyle.normal.textColor = Color.white;
            }

            Rect newPos = new Rect(position.x + 2, position.y + 1, 300, 25);
            EditorGUI.LabelField(newPos, titleText, titleStyle);

            return new Rect(newPos.x, position.y, 300, 25);
        }
        private Rect DrawTypeTextField(Rect position)
        {
            int indent = 10;
            Rect newPos = new Rect(position.x + indent, position.y + 25 + 2, 50, 18);
            EditorGUI.LabelField(newPos, typeSlzdProp.displayName);

            newPos = new Rect(newPos.x + newPos.width, newPos.y, 250, 18);

            EditorGUI.BeginChangeCheck();
            var stringValue = EditorGUI.TextField(newPos, typeValue);
            if (EditorGUI.EndChangeCheck())
                typeSlzdProp.stringValue = stringValue;

            return new Rect(position.x + indent, position.y + position.height, 300, 18);
        }
        private Rect DrawUnderOverFlows(SerializedProperty property, Rect position)
        {
            underflowSlzdProp = property.FindPropertyRelative("_underflow");
            overflowSlzdProp = property.FindPropertyRelative("_overflow");

            underflowValue = underflowSlzdProp.boolValue;
            overflowValue = overflowSlzdProp.boolValue;

            EditorGUI.BeginChangeCheck();

            Rect newPos = new Rect(position.x, position.y + position.height + 8, 150f, position.height);
            var boolValue = EditorGUI.Toggle(newPos, underflowSlzdProp.displayName, underflowValue);

            if (EditorGUI.EndChangeCheck())
            {
                underflowSlzdProp.boolValue = boolValue;
                UpdateCurrent(currentSlzdProp.floatValue);
            }


            EditorGUI.BeginChangeCheck();

            newPos = new Rect(newPos.x + newPos.width, newPos.y, 150f, position.height);
            boolValue = EditorGUI.Toggle(newPos, overflowSlzdProp.displayName, overflowValue);

            if (EditorGUI.EndChangeCheck())
            {

                overflowSlzdProp.boolValue = boolValue;
                UpdateCurrent(currentSlzdProp.floatValue);
            }


            return new Rect(position.x, newPos.y, 300, newPos.height);
        }
        private Rect DrawCurrentMax(SerializedProperty property, Rect position)
        {
            currentSlzdProp = property.FindPropertyRelative("_current");
            maxSlzdProp = property.FindPropertyRelative("_max");

            currentValue = currentSlzdProp.floatValue;
            maxValue = maxSlzdProp.floatValue;


            Rect newPos = new Rect(position.x, position.y + position.height + 4, 50f, position.height);
            EditorGUI.LabelField(newPos, currentSlzdProp.displayName + ":");

            newPos = new Rect(newPos.x + newPos.width, newPos.y, 100, 18);

            EditorGUI.BeginChangeCheck();
            var floatValue = float.Parse(EditorGUI.TextField(newPos, currentValue.ToString()));
            if (EditorGUI.EndChangeCheck())
                UpdateCurrent(floatValue);



            newPos = new Rect(newPos.x + newPos.width, newPos.y, 15f, position.height);
            EditorGUI.LabelField(newPos, "  / ");



            newPos = new Rect(newPos.x + newPos.width, newPos.y, 35f, position.height);
            EditorGUI.LabelField(newPos, maxSlzdProp.displayName + ":");

            newPos = new Rect(newPos.x + newPos.width, newPos.y, 100f, position.height);

            EditorGUI.BeginChangeCheck();
            floatValue = float.Parse(EditorGUI.TextField(newPos, maxValue.ToString()));
            if (EditorGUI.EndChangeCheck())
            {
                maxSlzdProp.floatValue = floatValue;
                UpdateCurrent(currentSlzdProp.floatValue);
            }


            return new Rect(position.x, newPos.y, 300, newPos.height);

        }

        private Gauge UpdateCurrent(float newCurrent)
        {
            Gauge check = new Gauge(
                                newCurrent,
                                maxSlzdProp.floatValue,
                                underflowSlzdProp.boolValue,
                                overflowSlzdProp.boolValue
                                );

            currentSlzdProp.floatValue = check.Current;

            return check;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (foldout)
                return 118f;
            else
                return 25 + 22;
        }
    }
}