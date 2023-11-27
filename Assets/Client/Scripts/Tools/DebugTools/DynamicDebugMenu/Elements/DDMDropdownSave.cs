using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements
{
    public class DDMDropdownSave : MonoBehaviour
    {
        [SerializeField]
        private Dropdown _dropdown;
        
        [SerializeField]
        private Text _label;
        
        [SerializeField]
        private Toggle _toggle;

        private Action<int> _onChange;
        private Action<bool> _onSave;

        public static void Create(DDMContext context, string label, Func<List<string>> optionsFunc, Func<int> initFunc, Action<int> onChange, bool initSave, Action<bool> onSave)
        {
            GameObject dropdownObject = Instantiate(context.Config.DropdownSavePrefab, context.Parent);
            DDMDropdownSave dropdown = dropdownObject.GetComponent<DDMDropdownSave>();

            dropdown._label.text = label;
            dropdown._onChange = onChange;
            dropdown._onSave = onSave;
            dropdown._dropdown.ClearOptions();
            dropdown._dropdown.AddOptions(optionsFunc.Invoke());
            dropdown._dropdown.value = initFunc.Invoke();
            dropdown._toggle.isOn = initSave;
        }

        private void Start()
        {
            _dropdown.onValueChanged.AddListener(OnValueChanged);
            _toggle.onValueChanged.AddListener(OnSaveChanged);
        }

        private void OnSaveChanged(bool isSave)
        {
            _onSave.Invoke(isSave);
        }

        private void OnValueChanged(int selectedValue)
        {
            _onChange.Invoke(selectedValue);
        }
    }
}