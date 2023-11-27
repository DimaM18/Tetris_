using System;

using UnityEngine;
using UnityEngine.UI;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements
{
    public class DDMInput : MonoBehaviour
    {
        [SerializeField]
        private InputField _input;
        
        [SerializeField]
        private Text _label;

        private Action<string> _onChange;

        public static void Create(DDMContext context, string label, InputField.ContentType contentType, Func<string> initFunc, Action<string> onChange)
        {
            Create(context, label, contentType, initFunc.Invoke(), onChange);
        }
        
        public static void Create(DDMContext context, string label, InputField.ContentType contentType, string initText, Action<string> onChange)
        {
            GameObject inputObject = Instantiate(context.Config.InputPrefab, context.Parent);
            DDMInput input = inputObject.GetComponent<DDMInput>();

            input._label.text = label;
            input._onChange = onChange;
            input._input.text = initText;
            input._input.contentType = contentType;
        }

        private void Start()
        {
            _input.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(string newText)
        {
            _onChange.Invoke(newText);
        }
    }
}