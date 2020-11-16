using UnityEngine;

namespace Scripts.MainframeReference
{
    [CreateAssetMenu(fileName = "new GameObjectCollection", menuName = "ScriptableObjects/GameObjectCollection")]
    public class GameObjectCollection : ScriptableObject
    {
        [SerializeField] private GameObject[] _gameObjects;

        public int Count { get => _gameObjects.Length; }
        public GameObject[] GameObjects { get => _gameObjects; }


        public static implicit operator GameObject[](GameObjectCollection goc) => goc._gameObjects;
        public GameObject this[int index]
        {
            get => _gameObjects[index];
            set => _gameObjects[index] = value;
        }
    }
}