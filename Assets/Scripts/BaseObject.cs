using UnityEngine;
using System.Globalization;
using System;
using System.Text;
using Scripts.MainframeReference;

namespace Scripts
{
    [System.Serializable]
    public abstract class BaseObject : MonoBehaviour
    {
        BaseObjectSaveData _saveData;

        public virtual int ID 
        { 
            get => _saveData._id; 
            protected set => _saveData._id = value; 
        }

        public virtual string Name         
        { 
            get => _saveData._name;  
            set => _saveData._name = value; 
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
    }

}