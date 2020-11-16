using UnityEngine;

namespace Scripts.MainframeReference
{
    public class ValueControl : TextControl
    {
        public static event System.Action<MonoBehaviour, object> Control_ValueChanged;

        [SerializeField] private TMPro.TMP_Text _value;
        public string Value
        {
            get => _value.text;
            set
            {
                _value.text = value;
                ValueChanged_Invoke(this, value);
            }
        }

        protected void ValueChanged_Invoke(MonoBehaviour c, object value) =>
            Control_ValueChanged?.Invoke(c, value);
    }
}