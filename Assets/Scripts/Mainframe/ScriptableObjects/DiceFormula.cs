using UnityEngine;

namespace Scripts.Mainframe.ScriptableObjects
{
    [CreateAssetMenu(fileName = "new DiceFormula", menuName = "ScriptableObjects/DiceFormula")]
    public class DiceFormula : ScriptableObject 
    {
        [SerializeField] private Dice[] _formula;

        public Dice[] Formula { get => _formula; set => _formula = value; }



        public static implicit operator Dice[](DiceFormula formula) => formula._formula;
        public Dice this[int index]
        {
            get => _formula[index];
            set => _formula[index] = value;
        }

    }
}