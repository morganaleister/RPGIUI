using UnityEngine;

namespace Scripts.Mainframe
{
    public class CharacterUpdate : UpdateData
    {
        public string _objectID;
        public CharacterUpdate(object sender, string objectID, string oldValue, string currentValue = "") 
            : base(sender, oldValue, currentValue)
        {
            _objectID = objectID;
        }
    }

}