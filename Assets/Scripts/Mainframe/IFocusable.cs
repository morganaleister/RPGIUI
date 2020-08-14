using System;
namespace Scripts.Mainframe
{
    public interface IFocusable
    {
        event Action Focused, Unfocused;
        bool IsFocused { get; }

        void Highlight();
        void Dehighlight();
    }
}