using System;
namespace Scripts.Mainframe
{
    public interface ISelectable : IHighlightable
    {
        event Action Selected, Deselected;
        bool IsSelected { get; }

        void Select();
        void Deselect();
    }
}