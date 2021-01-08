using Scripts.MainframeReference.Debug;
using System;
using UnityEngine;

namespace Scripts.MainframeReference
{
    public class SelectionManager : MonoBehaviour
    {
        public static SelectionManager Singleton { get; private set; }

        public static event Action<IHighlightable> ControlHighlighted, ControlLostHighlight;
        public static event Action<IPressable> ControlPressed, ControlReleased;
        
        private static IHighlightable _highlighted;
        private static IPressable _pressed;
        private static ISelectable _selected;
        public static IHighlightable Highlighted
        {
            get => _highlighted;
            private set
            {
                if (Highlighted != value) //if new value is different from the one already stored
                {
                    if (Highlighted != null) //if the stored one is NOT empty
                    {
                        //stored control looses the highlight and informs
                        ControlLostHighlight?.Invoke(Highlighted);

                        //Update to new value
                        _highlighted = value;

                        if (value != null) //if new value is highlightable 
                            ControlHighlighted?.Invoke(Highlighted);//Highlight it

                    }
                    else //stored one IS empty, but since stored != new value too, value is NOT empty therefore do
                    {
                        _highlighted = value; //update value
                        ControlHighlighted?.Invoke(Highlighted); //highlight
                    }

                }
            }
        }
        public static IPressable Pressed
        {
            get => _pressed;
            private set
            {
                if (value != null)
                {
                    if (_pressed != null)
                    {
                        Release();
                    }
                    _pressed = value;
                    ControlPressed?.Invoke(Pressed);
                }
                else //value == null
                {
                    var released = _pressed;
                    _pressed = null;
                    ControlReleased?.Invoke(released);
                }

            }
        }
        public static ISelectable Selected
        {
            get => _selected;
            set
            {

            }
        }

        void Awake()
        {
            if (!Singleton) Singleton = this;
            else if (Singleton != this) Destroy(gameObject);
        }

        private static T GetFromRay<T>()
        {
            var ray = Camera.main.ScreenPointToRay(MouseTracker.MousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {//hit something

                //return the asked component type or null
                return hit.transform.GetComponent<T>();
            }
            UnityEngine.Debug.DrawRay(ray.origin, ray.direction, Color.red, 3);

            return default(T);
        }
        public static void Highlight() => Highlighted = GetFromRay<IHighlightable>();
        public static void Press() => Pressed = GetFromRay<IPressable>();
        public static void Release() => Pressed = null;

        public static void Select() => Selected = GetFromRay<ISelectable>();



       

        public static bool IsSelected(ISelectable selectable)
        {
            throw new NotImplementedException();
        }
    }
}