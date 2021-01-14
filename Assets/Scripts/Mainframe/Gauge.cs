using UnityEngine;

namespace Scripts.MainframeReference
{
    /// <summary>
    /// A labeled float-pair field where one field dictates the max value of another, and that one also can't go below 0
    /// </summary>
    [System.Serializable]
    public class Gauge
    {
        public Informant OnValueChanged;

        public string _type;
        [SerializeField] private bool _underflow = false, _overflow = false;
        [SerializeField] private float _max = 0;
        [SerializeField] private float _current = 0;

        public string Type { get => _type; set => _type = value; }
        public bool Underflow { get => _underflow; set => _underflow = value; }
        public bool Overflow { get => _overflow; set => _overflow = value; }
        public float Max { get => _max; set => _max = value; }

        public float Current
        {
            get => _current;
            set
            {
                if (value == _current) return; //doesn't fire if not changed

                UpdateData update = new UpdateData(this, Normalized.ToString());

                if (value < 0)
                {   //if below range check option and proceed
                    if (Underflow) _current = value;
                    else _current = 0;
                }
                else if (value > Max)
                { //if abvove range check option and proceed
                    if (Overflow) _current = value;
                    else _current = Max;
                }
                else //if in range, proceed.
                {
                    _current = value;
                }
                    update._newValue = Normalized.ToString();
                    OnValueChanged?.Invoke(update);
                
            }
        }     
        public float Normalized { get => _current / Max; }
        public Vector2 Fraction { get => new Vector2(Current, Max); }

        public static implicit operator float(Gauge gauge) { return gauge._current; }


        public Gauge() { }
        
        public Gauge(float current, float max, bool underflow = false, bool overflow = false)         
        {
            Underflow = underflow;

            Overflow = overflow;

            Max = max; 

            Current = current; 
        }

    }
}