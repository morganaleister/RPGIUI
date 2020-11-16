using UnityEngine;
using Scripts.MainframeReference;
using System;

namespace Scripts
{
    [Serializable]
    public class Character : BaseObject
    {
        public static event Action<Character, string, float> AttributeValueChanged;
        public static event Action<Character, string, string> StringValueChanged;

        public override int ID { get => _characterData._id; protected set => _characterData._id = value; }

        private CharacterData _characterData = new CharacterData();

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
        public CharacterData GetCharacterData()
        {
            var ret = _characterData;
            return ret;
        }
        public void SetCharacterData(CharacterData newData) => _characterData = newData;

        
        protected override int CreateID(string seed)
        {
            int id = base.CreateID(seed);

            int[] attr = _characterData._attributesValues;
            int sum = 0;

            for (int i = 0; i < attr.Length; i++)
            {
                sum += (int)attr[i] * (i + 1);
            }

            id += sum;

            return id;
        }
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
        public void SetStringValue(string stringName, string stringValue)
        {
            int index = -1;
            for (int i = 0; i < Mainframe.CharStrings.Count; i++)
            {
                if (Mainframe.CharStrings.Keys[i] == stringName)
                {
                    index = i;
                    break;
                }
            }
            if (index < 0) return;
            if (_characterData._stringValues[index] != stringValue)
            {
                _characterData._stringValues[index] = stringValue;
                StringValueChanged?.Invoke(this, stringName, stringValue);
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
        public void SetAttributeValue(string attributeName, int value)
        {
            int index = -1;

            for (int i = 0; i < Mainframe.CharAttributes.Count; i++)
            {
                if (Mainframe.CharAttributes.Values[i] == attributeName)
                {
                    index = i;
                    break;
                }
            }
            if (index < 0) return;

            if (_characterData._attributesValues[index] != value)
            {
                _characterData._attributesValues[index] = value;
                AttributeValueChanged?.Invoke(this, attributeName, value);
            }
        }


        public static implicit operator CharacterData(Character character) => character.GetCharacterData();

        public void SetTempattrval() => SetAttributeValue(Mainframe.CharAttributes.Values[UnityEngine.Random.Range(0, 8 + 1)], UnityEngine.Random.Range(0, 12 + 1));

    }

}