using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Mainframe
{    
    public class BaseButton : MonoBehaviour, ISelectable
    {
        public UnityEvent actionOnHighlight, actionOnDehighlight,
            actionOnSelect, actionOnDeselect;

        private bool _isHighlighted, _isSelected;


        public event Action Highlighted;
        public event Action Dehighlighted;
        public event Action Selected;
        public event Action Deselected;

        public bool IsHighlighted
        {
            get { return _isHighlighted; }
            private set
            {
                _isHighlighted = value;
                if (_isHighlighted) Highlighted();
                else Dehighlighted();
            }
        }

        public bool IsSelected 
        {
            get { return _isSelected; }
            private set
            {
                _isSelected = value;
                if (_isSelected) Selected();
                else Deselected();

            }
        }

        private void Awake()
        {
            Highlighted += OnHighlight;
            Selected += OnSelect;
            Dehighlighted += OnDehighlight;
            Deselected += OnDeselect;
        }

        

        protected virtual void OnHighlight() => actionOnHighlight?.Invoke();
        protected virtual void OnDehighlight() => actionOnDehighlight?.Invoke();
        protected virtual void OnSelect() => actionOnSelect?.Invoke();
        protected virtual void OnDeselect() => actionOnDeselect?.Invoke();

        public void Highlight() => IsHighlighted = true;
        public void Dehighlight() => IsHighlighted = false;
        public void Select() => IsSelected = true;
        public void Deselect() => IsSelected = false;
    }
}