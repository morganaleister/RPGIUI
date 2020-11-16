using UnityEngine;

namespace Scripts.MainframeReference
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
                case MainframeReference.Multiplier.x1:
                    Multiplier = MainframeReference.Multiplier.x10;
                    multiplierField.text = "10";
                    break;
                case MainframeReference.Multiplier.x10:
                    Multiplier = MainframeReference.Multiplier.x100;
                    multiplierField.text = "100";
                    break;
                case MainframeReference.Multiplier.x100:
                    Multiplier = MainframeReference.Multiplier.x1;
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