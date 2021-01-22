using System;
namespace Scripts.Mainframe
{
    public interface ISelectable : IPressable
    {

        event Action Selected, Deselected;
        bool IsSelected { get; }

        void Select();
        void Deselect();
    }
}