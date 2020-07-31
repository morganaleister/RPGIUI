using System;

namespace Scripts.Mainframe
{
    public interface IHighlightable
    {
        event Action Highlighted, Dehighlighted;
        bool IsHighlighted { get; }


        void Highlight();
        void Dehighlight();
    }
}