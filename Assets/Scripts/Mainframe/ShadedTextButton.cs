using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scripts.Mainframe
{
    [ExecuteInEditMode]
    public class ShadedTextButton : BaseButton
    {
        [SerializeField] private string _text = "";
        [SerializeField] private float _size = 10;

        [SerializeField] private List<TMP_Text> _texts = new List<TMP_Text>();

        public string Text { 
            get => _text; 
            set
            {
                _text = value;
                Apply();
            } 
        }
        public float Size
        {
            get => _size;
            set
            {
                _size = value;
                Apply();
            }
        }


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
            foreach (TMP_Text t in _texts)
            {
                t.text = _text;
                t.fontSize = _size;
            }
        }
    }
}
