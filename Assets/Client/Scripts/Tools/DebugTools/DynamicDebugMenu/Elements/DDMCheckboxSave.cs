using System;

using UnityEngine;
using UnityEngine.UI;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements
{
    public class DDMCheckboxSave : MonoBehaviour
    {
        [SerializeField]
        private Toggle _toggle;
        
        [SerializeField]
        private Toggle _toggleSave;
        
        [SerializeField]
        private Text _label;

        private Action<bool> _onChange;
        private Action<bool> _onSave;

        public static void Create(DDMContext context, string label, Func<bool> initFunc, Action<bool> onChange, bool initSave, Action<bool> onSave)
        {
            Create(context, label, initFunc.Invoke(), onChange, initSave, onSave);
        }
        
        public static void Create(DDMContext context, string label, bool initValue, Action<bool> onChange, bool initSave, Action<bool> onSave)
        {
            GameObject checkboxObject = Instantiate(context.Config.CheckboxSavePrefab, context.Parent);
            DDMCheckboxSave checkbox = checkboxObject.GetComponent<DDMCheckboxSave>();

            checkbox._label.text = label;
            checkbox._onChange = onChange;
            checkbox._toggle.isOn = initValue;
            checkbox._onSave = onSave;
            checkbox._toggleSave.isOn = initSave;
        }

        private void Start()
        {
            _toggle.onValueChanged.AddListener(OnValueChanged);
            _toggleSave.onValueChanged.AddListener(OnSaveChanged);
        }

        private void OnValueChanged(bool value)
        {
            _onChange.Invoke(value);
        }
        
        private void OnSaveChanged(bool isSave)
        {
            _onSave.Invoke(isSave);
        }
    }
}