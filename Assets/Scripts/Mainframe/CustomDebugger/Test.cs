using UnityEngine.Events;
using UnityEngine;

namespace Scripts.MainframeReference.Debug
{
    public class Test : MonoBehaviour
    {
        public UnityEvent onTest;

        

        public static void Print(string text) => UnityEngine.Debug.Log(text);

        public void TestThrowsToGet()
        {
            Dice[] form = new Dice[2];
            form[0] = new Dice();
            form[0].size = 6;
            form[1] = new Dice();
            form[1].size = 6;


            for (int i = 0; i < 25; i++)
            {
                
                int ttg = ThrowsToGet(form, i);
                string text = string.Format("Throws to get {0}: {1}", i, ttg);


                UnityEngine.Debug.Log(text);
            }
            
        }

        public static int ThrowsToGet(Dice[] formula, int tryGet, int maxTries = 100000)
        {
            int[] results = new int[maxTries];
            int throwsToGet = 1;

            for (int i = 1; i < maxTries; i++, throwsToGet++)
            {
                if ((results[i] = Dice.ThrowSeveralSum(formula)) == tryGet) { return throwsToGet;}
                
            }

            return -1;
        }

    }
}
