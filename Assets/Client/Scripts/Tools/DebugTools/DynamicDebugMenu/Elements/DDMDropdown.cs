using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements
{
    public class DDMDropdown : MonoBehaviour
    {
        [SerializeField]
        private Dropdown _dropdown;
        
        [SerializeField]
        private Text _label;

        private Action<int> _onChange;

        public static void Create(DDMContext context, string label, Func<List<string>> optionsFunc, Func<int> initFunc, Action<int> onChange)
        {
            GameObject dropdownObject = Instantiate(context.Config.DropdownPrefab, context.Parent);
            DDMDropdown dropdown = dropdownObject.GetComponent<DDMDropdown>();

            dropdown._label.text = label;
            dropdown._onChange = onChange;
            dropdown._dropdown.ClearOptions();
            dropdown._dropdown.AddOptions(optionsFunc.Invoke());
            dropdown._dropdown.value = initFunc.Invoke();
        }

        private void Start()
        {
            _dropdown.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(int selectedValue)
        {
            _onChange.Invoke(selectedValue);
        }
    }
}