
using UnityEngine;

namespace Client.Scripts.Ui.Controllers
{
    public class LoseScreen : IPanelController
    {
        private Panel _panel;

        public void Init(Panel panel)
        {
            _panel = panel;
        }

        public void DeInit()
        {
            Object.Destroy(_panel.gameObject);
        } 
    }
}