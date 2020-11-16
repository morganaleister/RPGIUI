using System;
using UnityEngine;

namespace Scripts.MainframeReference
{
    [ExecuteAlways()]
    public class ColorSyncronizer : MonoBehaviour
    {
        public event Action ColorChanged;

        public bool getRendrsFrmChild = false;

        public SpriteRenderer[] _renderers;
        [SerializeField] private Color _color;

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                ColorChanged?.Invoke();
            }
        }


        public void OnGUI()
        {
            GetList();
            ColorizeList();
        }

        private void ColorizeList()
        {
            if (_renderers == null || _renderers.Length == 0)
                return;

            for (int i = 0; i < _renderers.Length; i++)
                _renderers[i].color = _color;
        }
        private void GetList() { if (getRendrsFrmChild) { _renderers = GetComponentsInChildren<SpriteRenderer>(); }; }

        void Awake() => ColorChanged += ColorizeList;

        public void Apply() => ColorizeList();

    }
}