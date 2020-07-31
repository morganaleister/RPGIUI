using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scripts.Mainframe
{
    [ExecuteInEditMode]
    public class ShadedTextButton : BaseButton
    {
        public string _text = "";

        [SerializeField] private List<TMP_Text> _texts = new List<TMP_Text>();


        private void OnValidate() => Apply();
        private void OnEnable() => Apply();

        private void Apply() 
        {
            TMP_Text[] cic = GetComponentsInChildren<TMP_Text>();
            if (_texts.Count != cic.Length)
            {
                _texts = new List<TMP_Text>();
                _texts.AddRange(cic);
            }
            foreach (TMP_Text t in _texts) t.text = _text;
        }
    }
}
