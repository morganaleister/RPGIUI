using UnityEngine;

namespace Scripts.Mainframe
{
    public class TextControl : MonoBehaviour
    {
        public static event System.Action<MonoBehaviour, object> Control_LabelChanged;

        [SerializeField] private TMPro.TMP_Text _label;

        public string Label
        {
            get => _label.text;
            set
            {
                _label.text = value;
                Control_LabelChanged?.Invoke(this, value);
            }
        }

        protected void LabelChanged_Invoke(MonoBehaviour c, object value) =>
            Control_LabelChanged?.Invoke(c, value);

    }
}