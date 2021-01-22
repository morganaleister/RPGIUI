using UnityEngine;
using Scripts.Mainframe.ScriptableObjects;

namespace Scripts.Mainframe
{
    public class LivingBeing : BaseObject
    {
        public Physiognomy _physiognomy = new Physiognomy();

        public LivingBeing(string name = "unnamed") : base(name) { }
        [System.Serializable]
        public new class SaveData : BaseObject.SaveData
        {
            public string[] _raceStrings = new string[1];
            public Gauge[] _gauges = new Gauge[1];
        }
    }

    public class Physiognomy
    {
        [SerializeField] private BodyPart[] _bodySlots;

        public bool IsVacant(int bodySlotIndex)
            => _bodySlots[bodySlotIndex] == null;

        public Physiognomy(int _partSlots = 2)
        {
            _bodySlots = new BodyPart[_partSlots];
        }

        [System.Serializable]
        public class SaveData : BaseObject.SaveData
        {
            private const string Physiognomy = "Physiognomy";
            public PhysiognomyDefinition physiognomyDefinition;

            public SaveData()
            {
                
                _nameStrings = new string[2];
                _nameStrings[0] = physiognomyDefinition._name + Physiognomy;
            }
        }
    }
}