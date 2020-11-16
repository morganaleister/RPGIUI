using UnityEngine;
using UnityEngine.UI;

namespace Scripts.MainframeReference.CustomDebugger
{
    public class CustomDebugger : MonoBehaviour
    {
        public static CustomDebugger Singleton { get; private set; }
        public static string Text
        {
            get => Singleton._CompText.text;
            set => Singleton._CompText.text = value;
        }


        [SerializeField] private Text _CompText;


        private void Awake()
        {
            if (!Singleton) Singleton = this;
            else Destroy(gameObject);
        }


    }
}