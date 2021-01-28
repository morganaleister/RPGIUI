using UnityEngine;

namespace Scripts.Mainframe
{
    /// <summary>
    /// A labeled float-pair field where one field dictates the max value of another, and that one also can't go below 0
    /// </summary>
    [System.Serializable]
    public class Gauge
    {
        public Informant OnCurrentValueChanged = new Informant();
        public Informant OnStatusChanged = new Informant();

        public string _type = "HP";
        public enum Status
        {
            Underflowing = -1,
            Normal,
            Overflowing
        }

        [SerializeField] private bool _underflow = false, _overflow = false;
        [SerializeField] private float _max = 0;
        [SerializeField] private float _current = 0;

        public string Type { get => _type; set => _type = value; }
        public bool Underflow { get => _underflow; set => _underflow = value; }
        public bool Overflow { get => _overflow; set => _overflow = value; }
        public float Max { get => _max; set => _max = value; }
        public Status CurrentStatus { get; protected set; } = Status.Normal;

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
                {   //if above range check option and proceed
                    if (Overflow) _current = value;
                    else _current = Max;
                }
                else //if in range, proceed.
                {
                    _current = value;
                }
                update._newValue = Normalized.ToString();
                OnCurrentValueChanged?.Invoke(update);

            }
        }
        public float Normalized { get => _current / Max; }
        public Vector2 Fraction { get => new Vector2(Current, Max); }


        public bool Undrflowing { get => _current < 0; }
        public bool Overflowing { get => _current > Max; }



        public static implicit operator float(Gauge gauge) { return gauge._current; }


        public Gauge() => new Gauge("[HP]", 1, 1, false, false);
        public Gauge(string type, float current = 1, float max = 1, bool underflow = false, bool overflow = false)
        {
            _type = type;

            Underflow = underflow;
            Overflow = overflow;
            Max = max;
            Current = current;

            OnCurrentValueChanged.AddListener(CheckStatus);
        }

        private void CheckStatus(UpdateData updateData)
        {
            float _old = float.Parse(updateData._oldValue);
            float _new = float.Parse(updateData._newValue);

            if (Underflow)
            {
                if(_old >= 0f && _new < 0)
                    UpdateInform(Status.Normal, Status.Underflowing);
              
                if (_old < 0f && _new <= 0)
                    UpdateInform(Status.Underflowing, Status.Normal);                
            }
            if (Overflow) 
            {
                if (_old <= Max && _new > Max)
                    UpdateInform(Status.Overflowing, Status.Normal);
                if (_old > Max && _new <= Max)
                    UpdateInform(Status.Normal, Status.Overflowing);
            }

            void UpdateInform(Status oldStatus, Status newStatus)
            {
                UpdateData update = new UpdateData(this, oldStatus.ToString(), newStatus.ToString());

                OnStatusChanged?.Invoke(update);
            }
        }
        


    }
}