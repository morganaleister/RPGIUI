using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Mainframe
{
    public class ColorSwitchButton : BaseButton
    {
        [SerializeField] private Renderer _target;

        [Serializable]
        public struct NamColor
        {
            public string _name;
            public Color _color;
        }

        [SerializeField]
        private List<NamColor> _switches =
            new List<NamColor>();
        private Color _lastColor;


        public void DoSwitch(Color color)
        {
            _lastColor = _target.material.color;
            _target.material.color = color;
        }

        public void SwitchToLast()
        {
            DoSwitch(_lastColor);
        }
        public void DoSwitch(int index)
        { if (_target) DoSwitch(_switches[index]._color); }
        public bool DoSwitch(string name)
        {
            for (int i = 0; i < _switches.Count; i++)
            {
                if (_target &&
                    _switches[i]._name == name)
                {
                    DoSwitch(_switches[i]._color);
                    return true;
                }
            }
            return false;
        }
    }
}