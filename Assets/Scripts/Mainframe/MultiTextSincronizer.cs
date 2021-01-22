using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scripts.Mainframe
{
    [ExecuteInEditMode]
    public class MultiTextSincronizer : MonoBehaviour
    {
        
        [SerializeField] private string _text = "";
        public bool updateText;
        [SerializeField] private float _size = 10;
        public bool _autoSize;
        public bool updateSize;
        
        

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

        public bool AutoSize
        {
            get => _autoSize;
            set
            {
                _autoSize = value;
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
                if(updateText) t.text = _text;
                if(updateSize) t.fontSize = _size;
                t.enableAutoSizing = AutoSize;
            }
        }
    }
}
