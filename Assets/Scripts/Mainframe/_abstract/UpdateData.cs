using UnityEngine;

namespace Scripts.MainframeReference
{
    [System.Serializable]
    public class UpdateData
    {       
        public object _sender;
        public string _oldValue, _newValue;

        public string Text { get =>
                string.Format("Object [{0}]'s value [{1}] now is [{1}]",
                    _sender, _oldValue, _newValue); }

        public UpdateData(object sender, string oldValue, string currentValue = "")
        {
            _sender = sender;
            _oldValue= oldValue ;
            _newValue = currentValue;
        }
    }

}