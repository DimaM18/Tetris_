using System;

using UnityEngine;
using UnityEngine.UI;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements
{
    public class DDMSubmenu : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        [SerializeField]
        private Text _label;

        private Action _action;

        public static void Create(DDMContext context, string label, Action action)
        {
            GameObject submenuObject = Instantiate(context.Config.SubmenuPrefab, context.Parent);
            DDMSubmenu submenu = submenuObject.GetComponent<DDMSubmenu>();

            submenu._label.text = "> " + label;
            submenu._action = action;
        }

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            _action.Invoke();
        }
    }
}