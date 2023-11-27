using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Client.Scripts.Ui.Bind
{
    public class InputBind : IBind
    {
        public event Action<string> Changed;
        public event Action<string> Submited;
        
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
        
        private InputField _text;
        private TMP_InputField _tmpText;
        
        public bool Init(GameObject gameObject)
        {
            _text = gameObject.GetComponent<InputField>();
            _tmpText = gameObject.GetComponent<TMP_InputField>();

            if (_text)
            {
                _text.onValueChanged.AddListener(OnValueChanged);
                _text.onEndEdit.AddListener(OnEndEdit);
            }

            if (_tmpText)
            {
                _tmpText.onValueChanged.AddListener(OnValueChanged);
                _tmpText.onEndEdit.AddListener(OnEndEdit);
            }

            return _text || _tmpText;
        }

        private void OnEndEdit(string newText)
        {
            Submited?.Invoke(newText);
        }

        private void OnValueChanged(string newText)
        {
            Changed?.Invoke(newText);
        }
    }
}