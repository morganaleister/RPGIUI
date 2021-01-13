using UnityEngine;

namespace Scripts.MainframeReference
{
    public class CharacterUpdate : UpdateData
    {
        public string _objectID;
        public CharacterUpdate(Object sender, string objectID, string oldValue, string currentValue = "") 
            : base(sender, oldValue, currentValue)
        {
            _objectID = objectID;
        }
    }

}