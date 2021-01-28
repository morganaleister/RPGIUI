using Scripts.Mainframe;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Gauge))]
public class GaugeDrawer : PropertyDrawer
{
    private SerializedProperty
        typeSlzdProp,
        currentSlzdProp, maxSlzdProp,
        underflowSlzdProp, overflowSlzdProp,
        eventValueChangedSlzdProp, eventStateChangedSlzdProp;
    private string typeValue;
    private bool
        mainFoldout = false,
        eventsFoldout = false;
    private float
        lineHeight, labelWidth;
    private readonly float vspace = 2f;
    private readonly float hoffset = 22f;
    private float
        currentWidth, currentIndent,
        eventValueHeight, eventStateHeight,
        eventValueExtraHeight, eventStateExtraHeight;
    private int eventValueSubs, eventStateSubs;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        lineHeight = EditorGUIUtility.singleLineHeight;
        labelWidth = EditorGUIUtility.labelWidth;
        currentWidth = EditorGUIUtility.currentViewWidth;
        currentIndent = EditorGUI.IndentedRect(position).x - position.x - vspace;

        Initialize(property);
        string titleText = string.Format("{0} ({1})", typeValue, property.type);

        //sets the Rect to draw the foldout title
        var leftPos = new Rect(EditorGUI.IndentedRect(position).x, position.y, currentWidth - currentIndent - hoffset, lineHeight);
        //EditorGUI.DrawRect(r, Color.white);

        mainFoldout = EditorGUI.BeginFoldoutHeaderGroup(leftPos, mainFoldout, titleText);
        {
            //EditorGUI.DrawRect(position, Color.black);
            DrawProgressBar(position);
            if (mainFoldout)
            {
                var indentLvl = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 1;
                bool underfoverf = true;

                DrawTypeTextField(leftPos, position);

                DrawValueElement(leftPos, 2,
                    maxSlzdProp.displayName, maxSlzdProp.floatValue,
                    overflowSlzdProp.displayName, overflowSlzdProp.boolValue,
                    underfoverf);

                underfoverf = false;

                DrawValueElement(leftPos, 3,
                    currentSlzdProp.displayName, currentSlzdProp.floatValue,
                    underflowSlzdProp.displayName, underflowSlzdProp.boolValue,
                    underfoverf);


                var r = new Rect(
                    leftPos.x,
                    leftPos.y + (lineHeight * 4) + (vspace * 4),
                    currentWidth - hoffset - currentIndent,
                    lineHeight);

                eventsFoldout = EditorGUI.Foldout(r, eventsFoldout, "Events");
                {

                    if (eventsFoldout) DrawEvents(r);


                }
                EditorGUI.indentLevel = indentLvl;
            }
            EditorGUI.EndFoldoutHeaderGroup();


        }
    }
    private void Initialize(SerializedProperty property)
    {
        typeSlzdProp = property.FindPropertyRelative("_type");
        typeValue = typeSlzdProp.stringValue.Trim();

        underflowSlzdProp = property.FindPropertyRelative("_underflow");
        overflowSlzdProp = property.FindPropertyRelative("_overflow");
        currentSlzdProp = property.FindPropertyRelative("_current");
        maxSlzdProp = property.FindPropertyRelative("_max");

        eventValueHeight = (lineHeight * 5) + (vspace * 2);
        eventStateHeight = eventValueHeight;

        eventValueChangedSlzdProp = property.FindPropertyRelative("OnCurrentValueChanged");
        eventStateChangedSlzdProp = property.FindPropertyRelative("OnStatusChanged");

        eventValueSubs = eventValueChangedSlzdProp.FindPropertyRelative("m_PersistentCalls.m_Calls").arraySize;
        eventStateSubs = eventStateChangedSlzdProp.FindPropertyRelative("m_PersistentCalls.m_Calls").arraySize;

        CalcEventsHeight();
    }
    private void DrawProgressBar(Rect position)
    {
        //sets the Rect to draw the ProgressBar
        var r = new Rect(
            position.x + labelWidth,
            position.y,
            currentWidth - labelWidth - (currentIndent / EditorGUI.indentLevel) - hoffset,
            lineHeight);

        Gauge value = UpdateCurrent(currentSlzdProp.floatValue);
        string display = string.Format("{0} / {1}", value.Fraction.x, value.Fraction.y);

        EditorGUI.ProgressBar(r, value.Normalized, display);
    }
    private void DrawTypeTextField(Rect leftPos, Rect rightPos)
    {
        //Set the rect to draw the label
        var r = new Rect(leftPos.x, leftPos.y + lineHeight + vspace, (labelWidth / 2), leftPos.height);
        //EditorGUI.DrawRect(r, Color.green);

        EditorGUI.LabelField(r, typeSlzdProp.displayName + ":");

        //Set the rect to draw the textField
        r = new Rect(r.x + (labelWidth / 2) + vspace, r.y, (labelWidth * 2) - hoffset, lineHeight);

        //EditorGUI.DrawRect(r, Color.red);

        EditorGUI.BeginChangeCheck();
        var stringValue = EditorGUI.TextField(r, typeValue);
        if (EditorGUI.EndChangeCheck())
            typeSlzdProp.stringValue = stringValue;
    }
    private void DrawValueElement(Rect pos, int floor,
        string f_displayName, float f_value,
        string b_displayName, bool b_value,
        bool underoverf)
    {
        var r = new Rect(pos.x, pos.y + (lineHeight * floor) + (vspace * floor), labelWidth, lineHeight);
        //EditorGUI.DrawRect(r, Color.Lerp(Color.green, Color.black, .7f));
        EditorGUI.LabelField(r, f_displayName + ":");


        r = new Rect(r.x + (labelWidth / 2) + vspace, r.y, labelWidth - hoffset, lineHeight);
        //EditorGUI.DrawRect(r, Color.Lerp(Color.red, Color.black, .7f));

        EditorGUI.BeginChangeCheck();

        float floatValue = EditorGUI.FloatField(r, f_value);

        if (EditorGUI.EndChangeCheck())
        {
            switch (underoverf)
            {
                case false:
                    currentSlzdProp.floatValue = floatValue;
                    UpdateCurrent(floatValue);
                    break;

                case true:
                    maxSlzdProp.floatValue = floatValue;
                    UpdateCurrent(currentSlzdProp.floatValue);
                    break;
            }

        }

        r = new Rect(r.x + r.width + vspace, r.y, labelWidth, r.height);
        //EditorGUI.DrawRect(r, Color.Lerp(Color.yellow, Color.black, .7f));
        EditorGUI.LabelField(r, b_displayName + ":");

        r = new Rect(r.x + (labelWidth / 2) + vspace, r.y, 50f, r.height);
        //EditorGUI.DrawRect(r, Color.Lerp(Color.blue, Color.white, .7f));

        EditorGUI.BeginChangeCheck();
        bool toggle = EditorGUI.Toggle(r, b_value);

        if (EditorGUI.EndChangeCheck())
        {
            switch (underoverf)
            {
                case false:
                    underflowSlzdProp.boolValue = toggle;
                    break;

                case true:
                    overflowSlzdProp.boolValue = toggle;
                    break;
            }
            UpdateCurrent(currentSlzdProp.floatValue);
        }
    }
    private void DrawEvents(Rect position)
    {
        CalcEventsHeight();

        var r = new Rect(
                       position.x + currentIndent,
                       position.y + lineHeight + vspace,
                       position.width - (vspace * 4) - currentIndent,
                       eventValueHeight + eventValueExtraHeight);

        //EditorGUI.DrawRect(r, Color.white);
        EditorGUI.PropertyField(r, eventValueChangedSlzdProp);

        r = new Rect(
            r.x,
            r.y + eventValueHeight + eventValueExtraHeight, //+ (lineHeight * 3) - ((vspace * 4) * (eventStateSubs - 1)) + (1 * (eventStateSubs - 1)),
            r.width,
            eventStateHeight + eventStateExtraHeight);

        //EditorGUI.DrawRect(r, Color.red);
        EditorGUI.PropertyField(r, eventStateChangedSlzdProp);
    }
    private void CalcEventsHeight()
    {
        if (eventValueSubs > 1)
        {
            eventValueExtraHeight =
                (eventValueSubs - 1) *
                (lineHeight * 3) - ((vspace * 4) * (eventValueSubs - 1)) + (1 * (eventValueSubs - 1));
        }
        else if(!(eventValueSubs > 1)) eventValueExtraHeight = 0;

        if (eventStateSubs > 1)
        {
            eventStateExtraHeight =
                (eventStateSubs - 1) *
                (lineHeight * 3) - ((vspace * 4) * (eventStateSubs - 1)) + (1 * (eventStateSubs - 1));
        }
        else if (!(eventStateSubs > 1)) eventStateExtraHeight = 0;
    }

    private Gauge UpdateCurrent(float newCurrent)
    {
        Gauge check = new Gauge("HP",
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
        if (mainFoldout)
        {
            if (eventsFoldout)
            {
                Initialize(property);
                CalcEventsHeight();

                return
                    (lineHeight * 5) + (vspace * 5) +
                    eventValueHeight + eventValueExtraHeight +
                    eventStateHeight + eventStateExtraHeight;
            }

            return (lineHeight * 5) + (vspace * 6);
        }

        else
            return lineHeight;
    }

}
