using System;
namespace Scripts.Controller
{
    public interface IHighlightable
    {
        event Action Highlighted, Dehighlighted;
        bool IsHighlighted { get; }


        void Highlight();
        void Dehighlight();
    }
}