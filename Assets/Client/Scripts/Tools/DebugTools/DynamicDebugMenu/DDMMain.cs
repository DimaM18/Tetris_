using System;
using System.Collections.Generic;

using Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements;
using Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Submenu;

using SRDebugger;

using UnityEngine;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu
{
    public class DDMMain : MonoBehaviour
    {
        private DebugItemsConfig _config;
        
        private readonly Stack<DDMPage> _pagesStack = new();
        private DDMPage _currentPage;

        private bool _isNeedRestart; 
        
        public void Init(DebugItemsConfig config)
        {
            _config = config;

            CreatePage("Main menu", MainMenuContent, true);
        }

        private void MainMenuContent(DDMContext context)
        {
            EmptySubmenu.Create(context);
            
            DDMButton.Create(context, "Console", () =>
            {
                SRDebug.Instance.ShowDebugPanel(DefaultTabs.Console);
            });
            
            DDMButton.Create(context, "Reset Progress", () =>
            {
                PlayerPrefs.DeleteAll();
            });
            
            DDMLabel.Create(context, GetBuildVersion());
        }

        public void CreatePage(string title, Action<DDMContext> contentGenerator, bool isRoot = false)
        {
            if (_currentPage)
            {
                _pagesStack.Push(_currentPage);
                _currentPage.gameObject.SetActive(false);
            }

            Action backAction = BackMenu;
            if (isRoot)
            {
                backAction = null;
            }
            
            DDMPage page = DDMPage.Create(_config, transform, title, backAction, CloseMenu);
            _currentPage = page;

            DDMContext context = new()
            {
                Config = _config,
                Main = this,
                Parent = page.ContentParent
            };
            contentGenerator?.Invoke(context);
        }

        private void BackMenu()
        {
            Destroy(_currentPage.gameObject);
            _currentPage = _pagesStack.Pop();
            _currentPage.gameObject.SetActive(true);
        }

        public void CloseMenu()
        {
            if (_isNeedRestart)
            {
                // TODO: restart
            }

            Destroy(gameObject);
        }

        public void SetRestart(bool restart = true)
        {
            _isNeedRestart = restart;
        }
        
        private string GetBuildVersion()
        {
            // TODO: version
            return "version";
        }
    }
}