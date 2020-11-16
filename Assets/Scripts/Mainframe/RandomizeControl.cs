namespace Scripts.MainframeReference
{
    public class RandomizeControl : ValueControl
    {
        public DiceFormula _diceFormula;

        public void Roll() => Value = Dice.ThrowSeveralSum(_diceFormula).ToString();

    }
}