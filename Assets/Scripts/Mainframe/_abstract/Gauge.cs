using UnityEngine;

namespace Scripts.MainframeReference
{
    [System.Serializable]
    public class Gauge
    {
        [SerializeField] private string _label;
        [SerializeField] private float _current, _max;

        public string Label { get => _label; set => _label = value; }
        public float Current 
        {
            get => _current; 
            set 
            {
                if (value <= Max)
                    _current = value;
                else //value bigger > than Max
                    _current = Max;
            } 
        }
        public float CurrentPercentage { get => (Current * 100) / _max; }
        public float Max { get => _max; set => _max = value; }
        


    }

}