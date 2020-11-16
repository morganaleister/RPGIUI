using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MainframeReference
{
    public class OverviewPages : MonoBehaviour
    {
        [SerializeField] private AttributeControl[] _attributeControls;
        [SerializeField] private TextControl[] _textControls;

        private void Start()
        {

            FillAttributes();
            FillStrings();
        }
        public void FillStrings()
        {
            TextControl[] _foundTextControls = GetComponentsInChildren<TextControl>();
            List<TextControl> _filteredTextControls = new List<TextControl>();
            var c = Mainframe.CharStrings.Count;

            foreach (var control in _foundTextControls)
            {

                for (int i = 0; i < c; i++)
                {
                    if (control.gameObject.name == Mainframe.CharStrings.Keys[i])
                        _filteredTextControls.Add(control);
                }
            }

            _textControls = _filteredTextControls.ToArray();
            TextControl.Control_LabelChanged += Control_LabelChanged;
        }
        private void Control_LabelChanged(MonoBehaviour textControl, object v)
        {
            var tc = (TextControl)textControl;

            if (tc.Label != null)
                CharacterPages.Target.SetStringValue(tc.gameObject.name, tc.Label);
        }

        public void FillAttributes()
        {
            int c = Mainframe.CharAttributes.Count;
            _attributeControls = GetComponentsInChildren<AttributeControl>();

            for (int i = 0; i < c; i++)
            {
                _attributeControls[i].Label = 
                    Mainframe.CharAttributes.Values[i];

                _attributeControls[i].Hint = 
                    Mainframe.CharAttributes.Values[i];

                _attributeControls[i].Value = 
                    CharacterPages.Target.GetAttributeValue
                    (_attributeControls[i].Label).ToString();
            }

            AttributeControl.Control_ValueChanged += Control_ValueChanged;
        }
        private void Control_ValueChanged(MonoBehaviour valueControl, object v)
        {
            var vc = (ValueControl)valueControl;

            if (vc.Label != null)
                CharacterPages.Target.SetAttributeValue(vc.Label, int.Parse(vc.Value));
        }


    }
}