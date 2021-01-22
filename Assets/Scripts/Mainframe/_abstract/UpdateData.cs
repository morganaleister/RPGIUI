using UnityEngine;

namespace Scripts.Mainframe
{
    [System.Serializable]
    public class UpdateData
    {       
        public object _sender;
        public string _oldValue, _newValue;

        public string Text { get =>
                string.Format("Object [{0}]'s old value [{1}] is now [{1}]",
                    _sender, _oldValue, _newValue); }

        public UpdateData(object sender, string oldValue, string currentValue = "")
        {
            _sender = sender;
            _oldValue= oldValue ;
            _newValue = currentValue;
        }
    }

}