using System;

using UnityEngine;
using UnityEngine.UI;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements
{
    public class DDMCheckbox : MonoBehaviour
    {
        [SerializeField]
        private Toggle _toggle;
        
        [SerializeField]
        private Text _label;

        private Action<bool> _onChange;

        public static void Create(DDMContext context, string label, Func<bool> initFunc, Action<bool> onChange)
        {
            Create(context, label, initFunc.Invoke(), onChange);
        }
        
        public static void Create(DDMContext context, string label, bool initValue, Action<bool> onChange)
        {
            GameObject checkboxObject = Instantiate(context.Config.CheckboxPrefab, context.Parent);
            DDMCheckbox checkbox = checkboxObject.GetComponent<DDMCheckbox>();

            checkbox._label.text = label;
            checkbox._onChange = onChange;
            checkbox._toggle.isOn = initValue;
        }

        private void Start()
        {
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(bool value)
        {
            _onChange.Invoke(value);
        }
    }
}