using UnityEngine.Events;
using Scripts.MainframeReference;
using System;

namespace Scripts
{
    [Serializable]
    public class Character : BaseObject
    {

        public Informant AttributeValueChanged;
        public Informant StringValueChanged;

        private CharData _characterData = new CharData();

        public override int ID 
        { 
            get => _characterData._id; 
            protected set => _characterData._id = value; 
        }        

        public void Awake()
        {
            InitializeValues();
        }

        private void InitializeValues()
        {
            int c = Mainframe.CharAttributes.Count;
            _characterData._attributesValues = new int[c];

            c = Mainframe.CharStrings.Count;
            _characterData._stringValues = new string[c];
            for (int i = 0; i < _characterData._stringValues.Length; i++)
                _characterData._stringValues[i] = string.Empty;  
        }

        public string GetFileName()
        {
            if (ID == 0) SetNewID();
            return ID.ToString(); ;
        }

        public string[] GetStringsValues() => _characterData._stringValues;

        /// <summary>
        /// Returns a copy of the CharacterData in current use by the Character object.
        /// </summary>
        /// <returns></returns>
        public CharData GetCharacterData()
        {
            var ret = _characterData;
            return ret;
        }
        public void SetCharacterData(CharData newData) => _characterData = newData;

        
        public override void SetNewID() => SetID(CreateID(GetExportFileName()));
        private string GetExportFileName()
        {
            string ret =
                _characterData._stringValues[0].Replace(" ", "") +
                _characterData._stringValues[1].Replace(" ", "") +
                _characterData._stringValues[2].Replace(" ", "");

            return ret;
        }
        public string GetFullName()
        {
            string 
                name = _characterData._stringValues[0].Replace(" ", ""),
                nname = _characterData._stringValues[1].Replace(" ", ""),
                sname = _characterData._stringValues[2].Replace(" ", "");

            if (nname != "") nname = ", '" + nname + "' ";
            else nname = " ";

            return name + nname + sname;
        }

        public string GetStringValue(int stringIndex) => _characterData._stringValues[stringIndex];
        public string GetStringValue(string stringName)
        {
            for (int i = 0; i < Mainframe.CharStrings.Count; i++)
            {
                if (Mainframe.CharStrings.Keys[i] == stringName)
                    return _characterData._stringValues[i];
            }
            throw new System.ArgumentOutOfRangeException();
        }
        public void SetStringValue(string stringName, string newValue)
        {
            int indexToModify = -1;
            for (int i = 0; i < Mainframe.CharStrings.Count; i++)
            {
                if (Mainframe.CharStrings.Keys[i] == stringName)
                {
                    indexToModify = i;
                    break;
                }
            }
            if (indexToModify < 0) return;
            if (_characterData._stringValues[indexToModify] != newValue)
            {
                CharacterUpdate updatedata = new CharacterUpdate
                    (this, stringName, _characterData._stringValues[indexToModify], newValue);

                _characterData._stringValues[indexToModify] = newValue;
                StringValueChanged?.Invoke(updatedata);
            }
        }
        public void SetStringValue(int stringIndex, string stringValue)
        {
            _characterData._stringValues[stringIndex] = stringValue;
        }

        public int[] GetAttributesValues() => _characterData._attributesValues;
        public float GetAttributeValue(int attributeIndex) => _characterData._attributesValues[attributeIndex];

       

        public float GetAttributeValue(string attributeName)
        {
            for (int i = 0; i < Mainframe.CharAttributes.Count; i++)
            {
                if (Mainframe.CharAttributes.Values[i] == attributeName)
                    return _characterData._attributesValues[i];
            }
            throw new System.ArgumentOutOfRangeException();
        }
        public void SetAttributeValue(string attributeName, int newValue)
        {
            int indexToModify = -1;

            for (int i = 0; i < Mainframe.CharAttributes.Count; i++)
            {
                if (Mainframe.CharAttributes.Values[i] == attributeName)
                {
                    indexToModify = i;
                    break;
                }
            }
            if (indexToModify < 0) return;

            if (_characterData._attributesValues[indexToModify] != newValue)
            {
                CharacterUpdate updatedata = new CharacterUpdate
                    (this, attributeName, 
                    _characterData._attributesValues[indexToModify].ToString(),
                    newValue.ToString());

                _characterData._attributesValues[indexToModify] = newValue;
                AttributeValueChanged?.Invoke(updatedata);
            }
        }


        public static implicit operator CharData(Character character) => character.GetCharacterData();

        public void SetTempattrval() => SetAttributeValue(Mainframe.CharAttributes.Values[UnityEngine.Random.Range(0, 8 + 1)], UnityEngine.Random.Range(0, 12 + 1));

    }

}