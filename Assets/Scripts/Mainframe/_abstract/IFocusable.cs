using System;
namespace Scripts.MainframeReference
{
    public interface IFocusable
    {
        event Action Focused, Unfocused;
        bool IsFocused { get; }
    }
}