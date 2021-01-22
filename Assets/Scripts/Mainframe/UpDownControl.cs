using UnityEngine;

namespace Scripts.Mainframe
{
    public class UpDownControl : ValueControl
    {
        [SerializeField] private TMPro.TMP_Text multiplierField;
        private float f_multiplier, f_value;
        
        public Multiplier Multiplier { get; private set; } = Multiplier.x100;
       
        void Awake()
        {
            GetFloats();
            CycleMultiplier();
        }
       
        private void GetFloats()
        {
            //get #
            f_value = float.Parse(Value, System.Globalization.NumberStyles.Float);
            //get multiplier
            f_multiplier = (float)Multiplier;
        }

        public void CycleMultiplier()
        {
            switch (Multiplier)
            {
                case Scripts.Mainframe.Multiplier.x1:
                    Multiplier = Scripts.Mainframe.Multiplier.x10;
                    multiplierField.text = "10";
                    break;
                case Scripts.Mainframe.Multiplier.x10:
                    Multiplier = Scripts.Mainframe.Multiplier.x100;
                    multiplierField.text = "100";
                    break;
                case Scripts.Mainframe.Multiplier.x100:
                    Multiplier = Scripts.Mainframe.Multiplier.x1;
                    multiplierField.text = "1";
                    break;
            }
            f_multiplier = (float)Multiplier;
        }

        public void Add()
        {
            GetFloats();
            f_value += f_multiplier;
            Value = f_value.ToString();
        }
        public void Substract()
        {
            GetFloats();
            f_value -= f_multiplier;
            Value = f_value.ToString();
        }
    }
}