using System;
using UnityEngine;
using System.Collections.Generic;

namespace Scripts
{
    [Serializable]
    public class CustomDictionary<T1, T2>
    {
        [SerializeField]
        private List<T1> _keys = new List<T1>();
        [SerializeField]
        private List<T2> _values = new List<T2>();

        public void Add(T1 key, T2 value)
        {
            _keys.Add(key);
            _values.Add(value);

        }

        public bool Remove(T1 key)
        {
            if(_keys.Count > 0)
            {

                for (int i = 0; i < _keys.Count; i++)
                {
                    if (_keys[i].Equals(key))
                    {
                        _keys.RemoveAt(i);
                        _values.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

    }
}