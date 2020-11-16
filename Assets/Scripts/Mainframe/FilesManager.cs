using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Scripts.MainframeReference
{
    public class FilesManager : MonoBehaviour
    {
        public static FilesManager Singleton { get; private set; }
        public static FileEncoding FileEncoding
        {
            get => Singleton._fileEncoding;
            set => Singleton._fileEncoding = value;
        }
        public static string SavesPath
        {
            get => Singleton._savesPath;
        }

        private string _savesPath;
        public FileEncoding _fileEncoding;





        public void Awake()
        {
            if (!Singleton) Singleton = this;
            if (Singleton != this) Destroy(gameObject);

            if (FileEncoding != FileEncoding.binary
                && FileEncoding != FileEncoding.Json)
                FileEncoding = FileEncoding.binary;

            _savesPath = Application.persistentDataPath;
        }

        public static CharacterData Load(FileEncoding fileType, string fullPath)
        {
            CharacterData charData = new CharacterData();

            using (FileStream fs = new FileStream(fullPath, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                switch (fileType)
                {
                    case FileEncoding.binary:

                        BinaryFormatter bf = new BinaryFormatter();
                        charData = (CharacterData)bf.Deserialize(fs);
                        break;
                    case FileEncoding.Json:

                        string json = sr.ReadToEnd();
                        charData = JsonUtility.FromJson<CharacterData>(json);
                        break;
                }
            }
            return charData;
        }

        public static string PathTo(string _folderName)
        {
            return SavesPath + Path.DirectorySeparatorChar + _folderName;
        }
        public static bool Save(Character character, FileEncoding fileType, bool overwrite = false, string fullPath = "")
        {
            string//s

                saveDir = SavesPath + "Characters",
                fileName = Path.DirectorySeparatorChar + character.GetFileName(),
                fileExt = ".char",

                fullSavePath = fullPath == "" ? saveDir + fileName + fileExt : fullPath;


            if (!Directory.Exists(saveDir))
            {
                string m = string.Format("Directory path [{0}] couldn't be found. " +
                    "Will be created and used from now on. " +
                    "Contents on another directory wont be loaded", saveDir);

                Debug.LogWarning(m);
                Directory.CreateDirectory(saveDir);
            }

            Debug.Log("SaveDirectory: " + saveDir);

            if (File.Exists(fullSavePath) && !overwrite)
                return false;

            using (FileStream fs = new FileStream(fullSavePath, FileMode.Create))
            {
                switch (fileType)
                {
                    case FileEncoding.binary:

                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(fs, character.GetCharacterData());
                        return true;
                    case FileEncoding.Json:

                        string json = JsonUtility.ToJson(character.GetCharacterData());
                        Write(fs, json);
                        return true;
                }
            }

            return false;
        }

        private static void Write(FileStream fs, string s) { using (StreamWriter sw = new StreamWriter(fs)) sw.Write(s); }
    }
}
