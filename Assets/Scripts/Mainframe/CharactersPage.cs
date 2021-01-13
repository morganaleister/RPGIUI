using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Scripts.MainframeReference
{
    public class CharactersPage : MonoBehaviour
    {
       [SerializeField] private GameObject _loadCharacterPrefab;
        
        private CharData[] _availableCharacters;

       
        
        private void Start() => Hide();
        public void Show() => this.enabled = true;
        public void Hide() => this.enabled = false;
        private void OnEnable() { GetCharacters(); FillSlots(); }

        public void RefreshPage() { GetCharacters(true); FillSlots(); }


        private void GetCharacters(bool refresh = false)
        {
            if (_availableCharacters != null && !refresh) return;
            
            var path = FilesManager.PathTo("Characters");
            List<CharData> charDatas = new List<CharData>();

            if (Directory.Exists(path))
            {
                string[] _files = Directory.GetFiles(path);
                for (int i = 0; i < _files.Length; i++)
                {
                    charDatas.Add(FilesManager.Load(FilesManager.FileEncoding, _files[i]));
                }
            }
            _availableCharacters = charDatas.ToArray();
        }

        private void FillSlots()
        {
            if (_availableCharacters == null || 
                _availableCharacters.Length == 0) return;



            for (int i = 0; i < _availableCharacters.Length; i++)
            {
                var go = Instantiate(_loadCharacterPrefab);
                var ch = go.GetComponent<Character>();
                var ctrl = go.GetComponent<SpritedValueControl>();

                ch.SetCharacterData(_availableCharacters[i]);

                ctrl.Label = ch.GetFullName();
                ctrl.Value = ch.GetFileName();




            }


        }



    }



}
