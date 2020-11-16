using Scripts.MainframeReference.CustomDebugger;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.MainframeReference
{
    public class BaseButton : MonoBehaviour, IPressable
    {
        public bool _pressEnabled = true;
        public UnityEvent actionOnPress, actionOnRelease;
        public bool IsPressed => Mainframe.Pressed == (IPressable)this;

        protected virtual void Awake()
        {
            Mainframe.ControlPressed += OnPress;
            Mainframe.ControlReleased += OnRelease;
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