using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MainframeReference
{
    [CreateAssetMenu(fileName = "new Creature Physiognomy", menuName = "ScriptableObjects/Creature Physiognomy")]
    public class CreaturePhysiognomy : ScriptableObject
    {
        [System.Serializable]
        public class physDescription
        {
            [SerializeField] private BodyPart bodyPart;
            [SerializeField] private int bpCount;
        }

        [SerializeField] private string race;
        [SerializeField] private List<physDescription> bodyParts;

        [SerializeField] private List<int> criticalIndexes;


    }
    [CreateAssetMenu(fileName = "new bodyPart", menuName = "ScriptableObjects/BodyPart")]
    public class BodyPart : ScriptableObject
    {
        public string bpName;
        public string pluralName;
        public WearSlot wearSlot;

        public Gauge HP = new Gauge() { _type = "HP", Current = 1, Max = 1 };
    }

    public class WearSlot
    {
        public IEquippable _object;
        public bool Vacant { get => !_object.Equipped; }
    }
    public class Item : BaseObject
    {
        public string Description { get; set; }
    }
    public interface IEquippable
    {
        bool Equipped { get; }
    }

}