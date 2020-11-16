using UnityEngine;

namespace Scripts.MainframeReference
{
    [System.Serializable]
    public class Dice
    {
        public int size;
        public Sprite sprite;

        public static int Throw(Dice dice) => Random.Range(1, dice.size + 1);
        public static int ThrowSeveralSum(Dice[] dices)
        {
            int c = dices.Length;
            int r = 0;

            for (int i = 0; i < c; i++)
            {
                r += dices[i].Throw();
            }

            return r;
        }
        public static int[] ThrowSeveral(Dice[] dices) 
        {
            int c = dices.Length;
            int[] r = new int[c];

            for (int i = 0; i < c; i++)
            {
                r[i] = dices[i].Throw();
            }
            return r;

        }
        public int Throw() => Throw(this);
        public int[] Throw_Times(int times = 1)
        {
            int[] r = new int[times];

            for (int i = 0; i < times; i++)
            {
                r[i] = Throw();
                Random.InitState((int)System.Math.Round(Random.value,0));
            }

            return r;
        }
        public int Throw_TimesSum(int times = 1)
        {
            int[] sum = Throw_Times(times);
            int r = 0;

            foreach (int item in sum)
            {
                r += item;
            }

            return r;
        }
    }
}