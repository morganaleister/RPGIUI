using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Scripts.Mainframe
{

    public class MainframeCommands : MonoBehaviour
    {
        public static MainframeCommands Singleton { get; private set; }

        [SerializeField] private List<SceneField> _sceneFields = new List<SceneField>();
        public static IHighlightable Highlighted { get; private set; }
        public static IPressable Pressed { get; private set; }
        public void Highlight()
        {
            var ray = Camera.main.ScreenPointToRay(MouseTracker.MousePos);
            RaycastHit hit;

            //debug
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 1);

            if (Physics.Raycast(ray, out hit))
            {//hit something

                //if hit is highlightable
                var thisHit = hit.transform.GetComponent<IHighlightable>();
                if (thisHit != null)
                {//SET Highlight Target 

                    IHighlightable lastHit = Highlighted; //updt last
                    Highlighted = thisHit; //updt current

                    if (lastHit != null)
                    {//check to not be the first one

                        //if last one is ! than current one
                        if (lastHit != Highlighted &&
                            !((IPressable)Highlighted).IsPressed)
                        {
                            lastHit.Dehighlight();//dehighlight old one
                            //and highlight current one
                            
                            Highlighted.Highlight();
                        } //else they are the same so skip
                    }
                    else//lastHit == null so its the first ever 
                        Highlighted.Highlight();
                }
            }
        }
        public void Press()
        {
            var ray = Camera.main.ScreenPointToRay(MouseTracker.MousePos);
            RaycastHit hit;

            //debug
            Debug.DrawRay(ray.origin, ray.direction, Color.green, 1);

            if (Physics.Raycast(ray, out hit))
            {//hit something

                //if hit is pressable
                IPressable thisHit = hit.transform.GetComponent<IPressable>();
                if (thisHit != null)
                {//SET press Target 

                    Pressed = thisHit;
                    thisHit.Press();
                }
            }

        }
        public void Release()
        {
            if (Pressed != null)
            {
                Pressed.Release();
                Pressed = null;
            }
        }


        public void LoadScene(int index) => SceneManager.LoadScene(Singleton._sceneFields[index]);



        private void Awake()
        {
            if (!Singleton) Singleton = this;
            else Destroy(gameObject);
        }
    }
}
