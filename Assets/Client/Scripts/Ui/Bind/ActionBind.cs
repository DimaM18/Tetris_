using System;

using UnityEngine;
using UnityEngine.UI;

namespace Client.Scripts.Ui.Bind
{
    public class ActionBind : IBind
    {
        public event Action Triggered;
        
        public bool Init(GameObject gameObject)
        {
            var button = gameObject.GetComponent<Button>();

            if (button)
            {
                button.onClick.AddListener(OnButtonClick);
                return true;
            }

            return false;
        }

        private void OnButtonClick()
        {
            Triggered?.Invoke();
        }
    }
}