using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.MainframeReference
{

    public class Mainframe : MonoBehaviour
    {
        public static Mainframe Singleton { get; private set; }
        public static event Action<IHighlightable> ControlHighlighted, ControlLostHighlight;
        public static event Action<IPressable> ControlPressed, ControlReleased;
        
        public static GameObject[] Prefabs => Singleton._prefabs;
        public static ListedStrDir CharAttributes => Singleton._charAttributes;
        
        public static KeysList CharStrings => Singleton._charStrings;
        
        private static IHighlightable _highlighted;
        private static IPressable _pressed;

        [SerializeField] private SceneField[] _scenes;
        [SerializeField] private GameObjectCollection _prefabs;

        [SerializeField] private DiceFormula _allDices;
        [SerializeField] private ListedStrDir _charAttributes;
        [SerializeField] private KeysList _charStrings;
        
        


        private void Awake() => Initialize();
        private void OnValidate() => Initialize();
        private void Initialize()
        {
            if (!Singleton) Singleton = this;

            CheckNulls();
        }
        private void CheckNulls()
        {
            string em = string.Format(
               "CharAttributes Mainframe.charStrings field cannot be null. \n");
            if (!CharAttributes) throw new System.ArgumentNullException(em);
        }

        public static GameObject GetPrefab(string name)
        {
            int c = Prefabs.Length;
            string[] _prefabsNames = new string[c]; ;

            for (int i = 0; i < c; i++)
            {
                _prefabsNames[i] = Prefabs[i].name;
            }


            for (int i = 0; i < _prefabsNames.Length; i++)
            {
                if (_prefabsNames[i] == name) return Singleton._prefabs[i];
            }

            return null;
        }


        public static IHighlightable Highlighted
        {
            get => _highlighted;
            private set
            {
                if (Highlighted != value) //if new value is different from the one already stored
                {
                    if (Highlighted != null) //if the stored one is NOT empty (then value is !null)
                    {
                        //stored control loose the highlight and informs
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
                        Singleton.Release();
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

        private static T GetFromRay<T>()
        {
            var ray = Camera.main.ScreenPointToRay(MouseTracker.MousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {//hit something

                //return the asked component type or null
                return hit.transform.GetComponent<T>();
            }
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 3);

            return default(T);
        }
        public void Highlight() => Highlighted = GetFromRay<IHighlightable>();
        public void Press() => Pressed = GetFromRay<IPressable>();
        public void Release() => Pressed = null;




        public void LoadScene(int index) => SceneManager.LoadScene(Singleton._scenes[index]);





        public void Test(string text) => CustomDebugger.Test.Print(text);


    }


}
