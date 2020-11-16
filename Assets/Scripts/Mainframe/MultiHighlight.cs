using UnityEngine;

namespace Scripts.MainframeReference
{
    public class MultiHighlight : ColorSyncronizer
    {
        public Color _HighlightColor;
        private Color _lastColor;
        public void Highlight()
        {
            _lastColor = Color;
            Color = _HighlightColor;
        }
        public void Dehighlight()
        {
            Color = _lastColor;
        }


    }
}