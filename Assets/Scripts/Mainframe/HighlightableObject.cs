using UnityEngine;
using UnityEngine.Events;

namespace Scripts.MainframeReference
{
    public class HighlightableObject : MonoBehaviour, IHighlightable
    {
        public UnityEvent actionOnHighlight, actionOnHighlightLoss;
        public bool IsHighlighted => SelectionManager.Highlighted ==(IHighlightable)this;

        void Awake()
        {
            SelectionManager.ControlHighlighted += OnHighlight;
            SelectionManager.ControlLostHighlight += OnHighlightLoss;
        }

        protected virtual void OnHighlight(IHighlightable obj)
        {
            if (obj == (IHighlightable)this)
                actionOnHighlight?.Invoke();
        }

        protected virtual void OnHighlightLoss(IHighlightable obj)
        {
            if (obj == (IHighlightable)this)
                actionOnHighlightLoss?.Invoke();
        }

    }
}
