using TMPro;
using UnityEngine;

namespace Scripts.MainframeReference
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text, y_text, n_text;
        private bool _pressed, _result;

        public string Text { get => _text.text; set => _text.text = value; }
        public string YesText { get => y_text.text; set => y_text.text = value; }
        public string NoText { get => n_text.text; set => n_text.text = value; }

        public static MessageBox Show(string message = "", string yes_text = "", string no_text = "")
        {
            var go = Instantiate(Mainframe.GetPrefab("MessageBox"));
            var msgbox = go.GetComponentInChildren<MessageBox>();


            if (message != "") msgbox.Text = message;

            if (yes_text != "") msgbox.YesText = yes_text;
            else msgbox.YesText = "Yes";

            if (no_text != "") msgbox.NoText = no_text;
            else msgbox.NoText = "No";

            go.SetActive(true);

            return msgbox;
        }
        public bool WasPressed { get => _pressed; }
        public bool Result { get => _result; }

        public void Press() => _pressed = true;
        public void Select() => _result = true;

        public void Kill() => Destroy(gameObject);

    }
}