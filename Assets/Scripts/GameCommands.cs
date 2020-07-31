using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Scripts.Mainframe
{
    public class GameCommands : MonoBehaviour
    {
        public static GameCommands singleton;
        [SerializeField] private SceneField sfPlayMenu, sfCreateMenu, sfDiceMenu;

        private static void s_CreateMenu() { SceneManager.LoadScene(singleton.sfCreateMenu); }
        private static void s_PlayMenu() { SceneManager.LoadScene(singleton.sfPlayMenu); }
        private static void s_DiceMenu() { SceneManager.LoadScene(singleton.sfDiceMenu); }


        public void PlayMenu() => s_PlayMenu();
        public void CreateMenu() => s_CreateMenu();
        public void DiceMenu() => s_DiceMenu();

        private void Awake()
        {
            if (singleton == null) singleton = this;
            else Destroy(gameObject);
        }
    }
}
