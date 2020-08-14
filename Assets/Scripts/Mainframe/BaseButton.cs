using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Mainframe
{
    public class BaseButton : MonoBehaviour, ISelectable, IFocusable
    {
        public UnityEvent
            actionOnHighlight, actionOnDehighlight,
            actionOnSelect, actionOnDeselect,
            actionOnPress, actionOnRelease,
            actionOnFocus, actionOnUnfocus;

        private bool 
            _isHighlighted, _isSelected, 
            _isPressed, _isFocused;

        public event Action 
            Highlighted, Dehighlighted, 
            Selected, Deselected, Pressed, 
            Released, Focused, Unfocused;

        public bool IsHighlighted
        {
            get => _isHighlighted;
            private set
            {
                _isHighlighted = value;
                if (_isHighlighted) Highlighted?.Invoke();
                else Dehighlighted?.Invoke();
            }
        }
        public bool IsSelected
        {
            get =>_isSelected;
            private set
            {
                _isSelected = value;
                if (_isSelected) Selected?.Invoke();
                else Deselected?.Invoke();

            }
        }
        public bool IsPressed
        {
            get => _isPressed;
            private set
            {
                _isPressed = value;
                if (_isPressed) Pressed?.Invoke();
                else Released?.Invoke();
            }
        }
        public bool IsFocused
        {
            get => _isFocused;
            private set
            {
                _isFocused = value;
                if (_isFocused) Focused?.Invoke();
                else Unfocused?.Invoke();
            }
        }


        private void Awake()
        {
            Highlighted += OnHighlight;
            Selected += OnSelect;
            Dehighlighted += OnDehighlight;
            Deselected += OnDeselect;
            Pressed += OnPress;
            Released += OnRelease;
            Focused += OnFocus;
            Unfocused += OnUnfocus;
        }

        protected virtual void OnHighlight() => actionOnHighlight?.Invoke();
        protected virtual void OnDehighlight() => actionOnDehighlight?.Invoke();
        protected virtual void OnSelect() => actionOnSelect?.Invoke();
        protected virtual void OnDeselect() => actionOnDeselect?.Invoke();
        protected virtual void OnPress() => actionOnPress?.Invoke();
        protected virtual void OnRelease() => actionOnRelease?.Invoke();
        protected virtual void OnFocus() => actionOnFocus?.Invoke();
        protected virtual void OnUnfocus() => actionOnUnfocus?.Invoke();


        public void Highlight() => IsHighlighted = true;
        public void Dehighlight() => IsHighlighted = false;
        public void Select() => IsSelected = true;
        public void Deselect() => IsSelected = false;
        public void Press() => IsPressed = true;
        public void Release() => IsPressed = false;
        public void Focus() => IsFocused = true;
        public void Unfocs() => IsFocused = false;

        public void Test() => Debug.Log("works");
    }
}