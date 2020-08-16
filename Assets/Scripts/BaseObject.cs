using UnityEngine;
using System.Globalization;
namespace Scripts
{
    public class BaseObject : MonoBehaviour
    {
        [SerializeField] protected Renderer[] picture;
        public long ID { get; private set; }
        public BaseObject(string idSeed)
        {
            SetIDfrom(this, idSeed);
        }
        private static long GetIDfrom(string idSeed)
        {

            char[] cc = idSeed.ToLowerInvariant().Trim().Replace(" ", "_").ToCharArray();

            long Return = cc.Length ^ 2;

            for (int i = 0; i < cc.Length; i++)
            {
                long unicode = (long) CharUnicodeInfo.GetNumericValue(cc[i]);
                long last;


                if (i == 0) last = 0;
                else last = (long) CharUnicodeInfo.GetNumericValue(cc[i - 1]);

                Return += (unicode * (i + 1)) - last;
            }

            return Return;
        }
        private static void SetIDfrom(BaseObject baseObject, string idSeed) => baseObject.ID = GetIDfrom(idSeed);
        protected virtual void SetID() => SetIDfrom(this, name);
        protected virtual void SetID(string from) => SetIDfrom(this, from);


    }

}