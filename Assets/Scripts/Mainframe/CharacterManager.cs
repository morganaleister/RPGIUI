using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MainframeReference
{
    public class CharacterManager : MonoBehaviour
    {
        public static event Action<Character> CharacterCreated, CharacterDeleted;

        public static CharacterManager Singleton;
        public static Character[] OnScene { get; private set; }
        
        public static Constraint _constraint;


        public void Awake()
        {
            if (!Singleton) Singleton = this;
            else if (Singleton != this) Destroy(this);
        }

        private void Start()
        {
            if (OnScene == null) OnScene = new Character[] { };
            CharacterCreated += OnCharacterCreated;
            CharacterDeleted += OnCharacterDeleted;
        }

        const string cs_Character = "fixthisshitnow";

        

        public static Character CreateCharacter(string name = cs_Character, GameObject _prefab = null)
        {
            GameObject go;

            if (!_prefab)
                go = Instantiate(Mainframe.GetPrefab(cs_Character));
            else
                go = Instantiate(_prefab);

            Character charObj = go.GetComponent<Character>();
            charObj.name = go.name = name;

            CharacterCreated?.Invoke(charObj);

            return charObj;
        }
        private static void OnCharacterCreated(Character obj)
        {
            List<Character> chars = new List<Character>();

            chars.AddRange(OnScene);
            chars.Add(obj);

            OnScene = chars.ToArray();
        }
        public static void DeleteCharacter(Character character)
        {
            CharacterDeleted?.Invoke(character);
            Destroy(character.gameObject);
        }       
        private static void OnCharacterDeleted(Character obj)
        {
            List<Character> chars = new List<Character>();

            chars.AddRange(OnScene);
            chars.Remove(obj);

            OnScene = chars.ToArray();
        }

    }


}
