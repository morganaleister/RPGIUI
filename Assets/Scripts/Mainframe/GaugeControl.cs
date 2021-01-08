using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MainframeReference
{
    public class GaugeControl : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text _label, _current, _max;

        public string Label { get => _label.text; set => _label.text = value; }
        public string Current { get => _current.text; set => _current.text = value; }
        public string Max { get => _max.text; set => _max.text = value; }

    }
}