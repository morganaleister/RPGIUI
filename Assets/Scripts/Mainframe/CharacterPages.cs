using System.Collections;
using UnityEngine;

namespace Scripts.Mainframe
{
    public class CharacterPages : MonoBehaviour
    {
        public static CharacterPages Singleton;
        public static Character Target => Singleton._target;

        [SerializeField] private Constraint _constraint;

        [SerializeField] private Character _target;

        private void Awake()
        {
            if (!Singleton) Singleton = this;
            else if (Singleton != this)
            {
                UnityEngine.Debug.Log(gameObject.ToString());
                Destroy(gameObject); //will it work?
                UnityEngine.Debug.Log(gameObject.ToString());
                UnityEngine.Debug.Log("If previous log reads error AndAlso first one didn't, Singleton pattern worked.");
            }
            if (!_target)
            {
                Character[] chars = FindObjectsOfType<Character>();

                if (chars.Length >= 1) _target = chars[0];
                else _target = CharacterManager.CreateCharacter();
            }
        }

        //public void Save()
        //{
        //  if (_target.GetStringValue(0) == string.Empty)
        //        throw new UnassignedReferenceException();

        //    if (!FilesManager.Save(_target, FilesManager.FileEncoding))
        //    {
        //        var msgbox = MessageBox.Show("File already exists, overwrite?");

        //        StartCoroutine(OverwriteYN(msgbox));
        //    };
        //}

        //private IEnumerator OverwriteYN(MessageBox msgbox)
        //{
        //    bool ans;

        //    yield return new WaitUntil(() => msgbox.WasPressed);
        //    ans = msgbox.Result;

        //    if (!ans)
        //    { //cancel if not authorized to overwrite

        //        UnityEngine.Debug.Log("Save operation sucessfully cancelled.");
        //        yield break;
        //    }
        //    else FilesManager.Save(_target, FilesManager.FileEncoding, ans);

        //    UnityEngine.Debug.Log("Save operation successful. File overwritten");

        //    yield break;
        //}

    }
}