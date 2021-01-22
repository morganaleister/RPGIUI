using UnityEngine.Events;
using UnityEditor;

namespace Scripts.Mainframe
{
    public class HighlightableButton : BaseButton, IHighlightable
    {
        public bool _highlightEnabled = true;
        public UnityEvent actionOnHighlight, actionOnHighlightLoss;
        
        public bool IsHighlighted => SelectionManager.Highlighted == (IHighlightable)this;

        protected override void Awake() 
        {
            base.Awake();
            SelectionManager.ControlHighlighted += OnHighlight;
            SelectionManager.ControlLostHighlight += OnHighlightLoss;
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
