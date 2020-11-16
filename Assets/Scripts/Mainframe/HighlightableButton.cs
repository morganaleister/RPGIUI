﻿using UnityEngine.Events;
using UnityEditor;

namespace Scripts.MainframeReference
{
    public class HighlightableButton : BaseButton, IHighlightable
    {
        public bool _highlightEnabled = true;
        public UnityEvent actionOnHighlight, actionOnHighlightLoss;
        
        public bool IsHighlighted => Mainframe.Highlighted == (IHighlightable)this;

        protected override void Awake() 
        {
            base.Awake();
            Mainframe.ControlHighlighted += OnHighlight;
            Mainframe.ControlLostHighlight += OnHighlightLoss;
        }

        protected virtual void OnHighlight(IHighlightable obj)
        {
            if(obj == (IHighlightable)this && _highlightEnabled)
                actionOnHighlight?.Invoke();
        }

        private void OnHighlightLoss(IHighlightable obj)
        {
            if (obj == (IHighlightable)this && _highlightEnabled)
                actionOnHighlightLoss?.Invoke();
        }
    }
}
