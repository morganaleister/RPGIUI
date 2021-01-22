using UnityEngine;
using System;
using System.Text;
using Scripts.Mainframe;

namespace Scripts
{
    [System.Serializable]
    public abstract class BaseObject : MonoBehaviour
    {
        SaveData _saveData = new SaveData();

        public virtual int ID 
        { 
            get => _saveData._id; 
            protected set => _saveData._id = value; 
        }

        public virtual string Name         
        { 
            get => _saveData._nameStrings[0];  
            set => _saveData._nameStrings[0] = value; 
        }

        public virtual void SetNewID() => SetID(CreateID(Name));

        protected virtual int CreateID(string idSeed)
        {
            if (idSeed == string.Empty)
                throw new ArgumentNullException("Field 'idSeed' can't be empty. CreateID Failed.");

            char[] chArray = idSeed.ToLowerInvariant().Trim().Replace(" ", "_").ToCharArray();
            byte[] chARRscii = Encoding.ASCII.GetBytes(chArray);

            int ret = chArray.Length ^ 2;
            int last;

            for (int i = 0; i < chArray.Length; i++)
            {
                int unicodeValue = chARRscii[i];

                if (i == 0) last = 0;
                else last = chARRscii[i - 1];

                ret += (unicodeValue * (i + 1)) - last;
            }

            return ret;
        }
        protected virtual void SetID(int id) => ID = id;

        protected BaseObject() 
        { 
            Name = "unnamed";
            SetNewID();
        }
        protected BaseObject(string name = "unnamed")
        {
            Name = name;
            SetNewID();
        }

        [System.Serializable]
        public class SaveData
        {
            public int _id;
            public string[] _nameStrings = new string[1];

        }
    }
}