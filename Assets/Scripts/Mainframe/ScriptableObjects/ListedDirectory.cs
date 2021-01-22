using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Mainframe.ScriptableObjects
{
    public class ListedDirectory<T> : ScriptableObject
    {
        [SerializeField] private string[] _keys;
        [SerializeField] private T[] _values;

        public virtual int Count { get => _keys.Length; }
        public virtual string[] Keys { get => _keys; }
        public T[] Values { get => _values; }

        /// <summary>
        /// Retrieves the index of the given key if exists. Returns -1 if not found.
        /// </summary>
        /// <param name="_key">key string to search for its index</param>
        /// <returns>Integer index from the given key string. -1 if not found</returns>
        public int GetIndex(string _key)
        {
            for (int i = 0; i < _keys.Length; i++)
            {
                if (_keys[i] == _key) return i;
            }
            return -1;
        }

        public static implicit operator string[](ListedDirectory<T> sc) => sc._keys;
               

        /// <summary>
        /// Retrieves the value pair from the given key if found. Throws error if not found.
        /// </summary>
        /// <param name="key">Key string to search for its pair value</param>
        /// <returns>Value pair from the given key. Error if not found</returns>
        public T GetValue(string key)
        {

            for (int i = 0; i < _keys.Length; i++)
            {
                if (_keys[i] == key) return _values[GetIndex(key)];
            }
            //this probably won ever be reached... but compiler complains if not written.
            throw new KeyNotFoundException();
        }
        public T this[int index]
        {
            get => _values[index];
            set => _values[index] = value;
        }
    }
}