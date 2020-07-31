using System;
namespace Scripts.Controller
{
    public interface ISelectable
    {
        event Action Selected, Deselected;
        bool IsSelected { get; }

        void Select();
        void Deselect();
    }
}