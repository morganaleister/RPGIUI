using System;
namespace Scripts.Mainframe
{
    public interface IPressable : IHighlightable
    {
        event Action Pressed, Released;
        bool IsPressed { get; }

        void Press();
        void Release();

    }
}