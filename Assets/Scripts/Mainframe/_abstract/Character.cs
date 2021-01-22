using Scripts.Mainframe;
using System;

namespace Scripts
{
    [Serializable]
    public class Character : LivingBeing
    {

        public Informant AttributeValueChanged;
        public Informant StringValueChanged;

        private Character.SaveData _characterData = new Character.SaveData();

        

        public Character(string name) : base(name)
        {

        }

        [System.Serializable]
        public new class SaveData : LivingBeing.SaveData
        {            
            public PortaitMode portrait;
        }
    }

}