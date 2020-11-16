using UnityEngine;

namespace Scripts.MainframeReference
{
    public class AttributeControl : ValueControl
    {
        [SerializeField] private TMPro.TMP_Text _hint;
        public string Hint { get => _hint.text; set => _hint.text = value; }

        protected virtual void Awake()
        {
            for (int i = 0; i < Mainframe.CharAttributes.Count; i++)
            {
                if (Mainframe.CharAttributes.Values[i] == Label)
                    Hint = Mainframe.CharAttributes.Values[i];
            }
        }
    }
}