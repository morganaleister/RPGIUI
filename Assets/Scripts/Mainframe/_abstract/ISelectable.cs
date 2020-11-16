using System;
namespace Scripts.MainframeReference
{
    public interface ISelectable : IPressable
    {
        event Action Selected, Deselected;
        bool IsSelected { get; }

        void Select();
        void Deselect();
    }
}