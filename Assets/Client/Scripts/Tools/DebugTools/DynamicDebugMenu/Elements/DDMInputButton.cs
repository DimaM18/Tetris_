using System;

using UnityEngine;
using UnityEngine.UI;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements
{
    public class DDMInputButton : MonoBehaviour
    {
        [SerializeField]
        private InputField _input;
        
        [SerializeField]
        private Button _button;
        
        [SerializeField]
        private Text _label;
        
        [SerializeField]
        private Text _buttonLabel;

        private Action<string> _onClick;

        public static void Create(DDMContext context, string label, string buttonLabel, InputField.ContentType contentType, Func<string> initFunc, Action<string> onClick)
        {
            Create(context, label, buttonLabel, contentType, initFunc.Invoke(), onClick);
        }
        
        public static void Create(DDMContext context, string label, string buttonLabel, InputField.ContentType contentType, string initText, Action<string> onClick)
        {
            GameObject inputObject = Instantiate(context.Config.InputButtonPrefab, context.Parent);
            DDMInputButton input = inputObject.GetComponent<DDMInputButton>();

            input._label.text = label;
            input._buttonLabel.text = buttonLabel;
            input._onClick = onClick;
            input._input.text = initText;
            input._input.contentType = contentType;
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _onClick.Invoke(_input.text);
        }
    }
}