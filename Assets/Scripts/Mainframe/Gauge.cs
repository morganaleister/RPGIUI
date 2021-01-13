using UnityEngine;

namespace Scripts.MainframeReference
{
    /// <summary>
    /// A labeled float-pair field where one field dictates the max value of another, and that one also can't go below 0
    /// </summary>
    [System.Serializable]
    public class Gauge : MonoBehaviour
    {
        public Informant OnValueChanged;

        public string _type;
        [SerializeField] private bool _uflow, _oflow;
        [SerializeField] private float _max;
        [SerializeField]
        private float
            _current = 0;

        public string Type { get; set; }
        public bool Underflow { get; set; }
        public bool Overflow { get; set; }
        public float Max { get; set; }

        public float Current
        {
            get => _current;
            set
            {
                if (value == _current) return; //doesn't fire if not changed

                UpdateData update = new UpdateData(this, Normalized.ToString());

                if (value > 0 && value <= Max)
                {
                    _current = value;
                    goto inform_current;
                } //if in range, skip other checks

                if (value < 0)
                {
                    if (Underflow) _current = value;
                    else _current = 0;
                }
                if (value > Max)
                {
                    if (Overflow) _current = value;
                    else _current = Max;
                }

            inform_current:
                update._newValue = Normalized.ToString();
                OnValueChanged?.Invoke(update);                
            }
        }     
        public float Normalized { get => _current / Max; }

        public static implicit operator float(Gauge gauge) { return gauge._current; }

    }

}