using System;

using UnityEngine;
using UnityEngine.UI;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements
{
    public class DDMButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        [SerializeField]
        private Text _label;

        private Action _onClick;

        public static void Create(DDMContext context, string label, Action onClick)
        {
            GameObject buttonObject = Instantiate(context.Config.ButtonPrefab, context.Parent);
            DDMButton button = buttonObject.GetComponent<DDMButton>();

            button._label.text = label;
            button._onClick = onClick;
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _onClick.Invoke();
        }
    }
}