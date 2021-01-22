using Scripts.Mainframe.Debuggering;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Mainframe
{
    public class BaseButton : MonoBehaviour, IPressable
    {
        public bool _pressEnabled = true;
        public UnityEvent actionOnPress, actionOnRelease;
        public bool IsPressed => SelectionManager.Pressed == (IPressable)this;

        protected virtual void Awake()
        {
            SelectionManager.ControlPressed += OnPress;
            SelectionManager.ControlReleased += OnRelease;
        }
        protected virtual void OnPress(IPressable obj)
        {
            if (obj == (IPressable)this && _pressEnabled)
                actionOnPress?.Invoke();
        }
        protected virtual void OnRelease(IPressable obj)
        {
            if (obj == (IPressable)this && _pressEnabled)
                actionOnRelease?.Invoke();

            
        }
    }
}