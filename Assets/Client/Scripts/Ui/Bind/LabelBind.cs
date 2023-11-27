using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Client.Scripts.Ui.Bind
{
    public class LabelBind : IBind
    {
        public string Text
        {
            get => _text ? _text.text : _tmpText.text;
            set
            {
                if (_text)
                {
                    _text.text = value;
                }
                else
                {
                    _tmpText.text = value;
                }
            }
        }
        
        private Text _text;
        private TMP_Text _tmpText;
        
        public bool Init(GameObject gameObject)
        {
            _text = gameObject.GetComponent<Text>();
            _tmpText = gameObject.GetComponent<TMP_Text>();

            return _text || _tmpText;
        }
    }
}