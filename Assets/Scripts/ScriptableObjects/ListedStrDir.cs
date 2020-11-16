using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MainframeReference
{
    [CreateAssetMenu(fileName = "new ListedStringDirectory", menuName = "ScriptableObjects/ListedStringDirectory")]
    public class ListedStrDir : KeysList
    {
        [SerializeField] private string[] _values;
        public string[] Values { get => _values; }

        /// <summary>
        /// Retrieves the value pair from the given key if found. Throws error if not found.
        /// </summary>
        /// <param name="key">Key string to search for its pair value</param>
        /// <returns>Value pair from the given key. Error if not found</returns>
        public string GetValue(string key)
        {

            for (int i = 0; i < _values.Length; i++)
            {
                if (_values[i] == key) return _values[GetIndex(key)];
            }
            //this probably won ever be reached... but compiler complains if not written.
            throw new KeyNotFoundException();
        }
        public override string this[int index]
        {
            get => _values[index];
            set => _values[index] = value;
        }
    }
    
}