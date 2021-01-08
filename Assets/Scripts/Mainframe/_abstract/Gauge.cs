using UnityEngine;
using UnityEngine.Events;

namespace Scripts.MainframeReference
{
    [System.Serializable]
    public class Informant : UnityEvent<InformantObject>
    {
    }
    [System.Serializable]
    public struct InformantObject
    {
        public Object _object;
        public float _value;
        public string _text;

        public InformantObject(Object o, float v, string t)
        {
            _object = o;
            _value = v;
            _text = t;
        }
    }

    /// <summary>
    /// A labeled float-pair field where one field dictates the max value of another, and that one also can't go below 0
    /// </summary>
    [System.Serializable]
    public class Gauge : MonoBehaviour
    {
        public Informant ValueChanged;

        public string _type;
        [SerializeField]
        private float
            _current = 0;
        public bool
            _underflow = false,
            _overflow = false;

        public string Type { get; set; }
        public bool Underflow { get; set; }
        public bool Overflow { get; set; }
        public float Max { get; set; }

        public float Current
        {
            get => _current;
            set
            {
                if (value == _current) return; //so event doesn't fire


                if (value <= Max && value >=)
                    _current = value;//just assign
                else  //value bigger > than Max
                {
                    if(Overflow)//overflow allowed
                    { 
                        _current = Max;//can't get bigger than max
                }
                if (_current < 0)
                    _current = 0; //but won't go below 0


                string _info = string.Format("{0} {1}", ToString(), Normalized);
                InformantObject io = new InformantObject(this, Normalized, _info);
                ValueChanged?.Invoke(io);
            }
        }

        public float Normalized { get => _current / _max; }

        public static implicit operator float(Gauge gauge) { return gauge._current; }




    }

}