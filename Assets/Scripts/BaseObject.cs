using UnityEngine;
using System.Globalization;
namespace Scripts
{
    public class BaseObject : MonoBehaviour
    {
        private long i_id;
        public long ID { get => i_id; }
        public BaseObject(string idSeed)
        {
            SetIDfrom(this, idSeed);
        }
        private static long GetIDfrom(string idSeed)
        {

            char[] cc = idSeed.ToLowerInvariant().Trim().Replace(" ", "_").ToCharArray();

            long res = cc.Length ^ 2;

            for (int i = 0; i < cc.Length; i++)
            {
                long unicode = (long) CharUnicodeInfo.GetNumericValue(cc[i]);
                long last;


                if (i == 0) last = 0;
                else last = (long) CharUnicodeInfo.GetNumericValue(cc[i - 1]);

                res += (unicode * (i + 1)) - last;
            }

            return res;
        }
        private static void SetIDfrom(BaseObject baseObject, string idSeed) => baseObject.i_id = GetIDfrom(idSeed);
        protected void SetID(string idSeed) => SetIDfrom(this, idSeed);


    }

}