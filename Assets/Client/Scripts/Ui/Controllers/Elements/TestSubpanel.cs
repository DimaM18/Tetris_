using Client.Scripts.DataStorage;
using Client.Scripts.Tools.Services;
using Client.Scripts.Ui.Bind;

using UnityEngine;

namespace Client.Scripts.Ui.Controllers.Elements
{
    public class TestSubpanel : ISubpanelController
    {
        private Panel _panel;
        private ActionBind _addAction;
        private ActionBind _subtractAction;
        private ActionBind _showAction;
        private ActionBind _hideAction;
        private LabelBind _counterLabel;
        private GameObjectBind _testObject;
        private InputBind _testInput;
        
        private int _counter;
        
        public void Init(Panel panel)
        {
            _panel = panel;

            _addAction = _panel.Bind<ActionBind>("AddButton");
            _subtractAction = _panel.Bind<ActionBind>("SubtractButton");
            _showAction = _panel.Bind<ActionBind>("ShowButton");
            _hideAction = _panel.Bind<ActionBind>("HideButton");
            _counterLabel = _panel.Bind<LabelBind>("CounterLabel");
            _testObject = _panel.Bind<GameObjectBind>("TestObject");
            _testInput = _panel.Bind<InputBind>("TestInput");
            
            _addAction.Triggered += OnAddClick;
            _subtractAction.Triggered += OnSubtractClick;
            _showAction.Triggered += OnShowClick;
            _hideAction.Triggered += OnHideClick;

            _testInput.Changed += text => Debug.Log("changed: " + text); 
            _testInput.Submited += text => Debug.Log("submited: " + text); 

            _counterLabel.Text = _counter.ToString();
        }

        public void DeInit()
        {
            _addAction.Triggered -= OnAddClick;
            _subtractAction.Triggered -= OnSubtractClick;
            _showAction.Triggered -= OnShowClick;
            _hideAction.Triggered -= OnHideClick;
            
            Object.Destroy(_panel.gameObject);
        }

        private void OnHideClick()
        {
            _testObject.GameObject.SetActive(false);
            
            Service.AudioManager.EnableSounds(false);
        }

        private void OnShowClick()
        {
            _testObject.GameObject.SetActive(true);
            
            Service.AudioManager.EnableSounds(true);
        }

        private void OnSubtractClick()
        {
            _counter--;
            _counterLabel.Text = _counter.ToString();
        }

        private void OnAddClick()
        {
            _counter++;
            _counterLabel.Text = _counter.ToString();
        }
    }
}