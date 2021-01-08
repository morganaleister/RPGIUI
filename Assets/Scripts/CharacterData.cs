using UnityEngine;
using Scripts.MainframeReference;

namespace Scripts
{
    [System.Serializable]
    public class CharacterData
    {
        

        public int _id;
        public string[] _stringValues;
        public int[] _attributesValues;

        public PlayerHead head;
        public Sprite sprite;

        public Effect[] _effects;
        public Gauge[] _gauges;

    }
}