using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.MainframeReference
{

    public class Mainframe : MonoBehaviour
    {
        public static Mainframe Singleton { get; private set; }
       
        
        public static GameObject[] Prefabs => Singleton._prefabs;
        public static ListedStrDir CharAttributes => Singleton._charAttributes;
        
        public static KeysList CharStrings => Singleton._charStrings;
        

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




        public void LoadScene(int index) => SceneManager.LoadScene(Singleton._scenes[index]);





        public void Test(string text) => Debug.Test.Print(text);


    }


}
