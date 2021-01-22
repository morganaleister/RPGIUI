using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Mainframe.Debuggering
{
    [ExecuteAlways()]
    [RequireComponent(typeof(AutoSorter))]
    public class Debugger : MonoBehaviour
    {
        public static Debugger Singleton { get; private set; }

        [SerializeField] private GameObject _linePrefab;
        private Dictionary<string, TextControl> _lines = new Dictionary<string, TextControl>();
        private Dictionary<string, List<TextControl>> _groups = new Dictionary<string, List<TextControl>>();

        private readonly AutoSorter autoSorter = null;

        private void Start()
        {
            if (!Singleton) Singleton = this;
            else if (Singleton != this) Destroy(this);
        }

        public static TextControl Write(string key, string text, string group = "")
        {
            TextControl line;

            if (Singleton._lines.TryGetValue(key, out line)) line.Label = text;
            else line = Create(key, text);

            if (group != "")
            {
                List<TextControl> controlGroup = new List<TextControl>();

                if (Singleton._groups.TryGetValue(group, out controlGroup)) controlGroup.Add(line);
                else
                {
                    controlGroup.Add(line);
                    Singleton._groups.Add(group, controlGroup);
                }
            }

            return line;
        }
        private static TextControl Create(string key, string text)
        {
            var go = Instantiate(Singleton._linePrefab);
            var tc = go.GetComponent<TextControl>();

            Singleton.autoSorter.Add(go.transform);
            Singleton._lines.Add(key, tc);

            tc.Label = text;

            return tc;
        }

    }
}