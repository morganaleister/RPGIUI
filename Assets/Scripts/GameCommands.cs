using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Mainframe
{
    public class GameCommands : MonoBehaviour
    {
        [SerializeField] List<SceneField> _sceneList = new List<SceneField>();

        private static void s_CreateMenu() { Debug.Log("CreateMenu selected"); }
        private static void s_PlayMenu() { Debug.Log("PlayMenu selected"); }
        private static void s_DiceMenu() { Debug.Log("DiceMenu selected"); }


        public void PlayMenu() => s_PlayMenu();
        public void CreateMenu() => s_CreateMenu();
        public void DiceMenu() => s_DiceMenu();


    }
}
