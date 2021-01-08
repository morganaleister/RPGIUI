using UnityEngine;

namespace Scripts.MainframeReference
{
    [CreateAssetMenu(fileName = "new KeyList", menuName = "ScriptableObjects/KeyList")]
    public class KeysList : ScriptableObject
    {
        [SerializeField] private string[] _keys;

        public virtual int Count { get => _keys.Length; }
        public virtual string[] Keys { get => _keys; }

        /// <summary>
        /// Retrieves the index of the given key if exists. Returns -1 if not found.
        /// </summary>
        /// <param name="_key">key string to search for its index</param>
        /// <returns>Integer index from the given key string. -1 if not found</returns>
        public int GetIndex (string _key)
        {
            for (int i = 0; i < _keys.Length; i++)
            {
                if (_keys[i] == _key) return i;
            }
            return -1;
        }

        public static implicit operator string[](KeysList sc) => sc._keys;
        public virtual string this[int index]
        {
            get => _keys[index];
            set => _keys[index] = value;
        }

    }
}