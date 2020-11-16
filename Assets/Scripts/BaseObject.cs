using UnityEngine;
using System.Globalization;
using System;
using System.Text;

namespace Scripts
{
    [System.Serializable]
    public class BaseObject : MonoBehaviour
    {
        private int _id;
        public virtual int ID { get => _id; protected set => _id = value; }
        public virtual void SetNewID() => SetID(CreateID(gameObject.name));

        protected virtual int CreateID(string idSeed)
        {

            char[] cc = idSeed.ToLowerInvariant().Trim().Replace(" ", "_").ToCharArray();
            byte[] ascii = Encoding.ASCII.GetBytes(cc);

            int ret = cc.Length ^ 2;
            int last;

            for (int i = 0; i < cc.Length; i++)
            {
                int unicode = ascii[i];

                if (i == 0) last = 0;
                else last = ascii[i];

                ret += (unicode * (i + 1)) - last;
            }

            return ret;
        }
        protected virtual void SetID(int id) => ID = id;
    }

}