using System;

using UnityEngine;
using UnityEngine.UI;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements
{
    public class DDMPage : MonoBehaviour
    {
        [SerializeField]
        private Button _backButton;
        
        [SerializeField]
        private Button _closeButton;
        
        [SerializeField]
        private Text _header;

        [SerializeField]
        private Transform _contentParent;

        private Action _backAction;
        private Action _closeAction;
        
        public Transform ContentParent => _contentParent;

        public static DDMPage Create(DebugItemsConfig config, Transform parent, string label, Action backAction, Action closeAction)
        {
            GameObject pageObject = Instantiate(config.PagePrefab, parent);
            DDMPage page = pageObject.GetComponent<DDMPage>();

            page._header.text = label;
            page._backAction = backAction;
            page._closeAction = closeAction;

            return page;
        }

        private void Start()
        {
            if (_backAction != null)
            {
                _backButton.onClick.AddListener(OnBackClick);
            }
            else
            {
                _backButton.gameObject.SetActive(false);
            }

            _closeButton.onClick.AddListener(OnCloseClick);
        }

        private void OnCloseClick()
        {
            _closeAction.Invoke();
        }

        private void OnBackClick()
        {
            _backAction.Invoke();
        }
    }
}