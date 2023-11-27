using Sirenix.OdinInspector;

using UnityEngine;

namespace Client.Scripts.Ui
{
    [CreateAssetMenu(fileName = "UiPrefabs", menuName = "Configs/UiPrefabs")]
    public class UiPrefabs : ScriptableObject
    {
        [SerializeField]
        private GameObject _canvasPrefab;
        
        [SerializeField]
        private GameObject _cameraPrefab;

        [SerializeField]
        [ListDrawerSettings(ListElementLabelName = "Id")]
        private PanelPrefabLink[] _panelPrefabLinks;

        [SerializeField]
        [ListDrawerSettings(ListElementLabelName = "Id")]
        private PanelPrefabLink[] _subpanelPrefabLinks;

        public GameObject CanvasPrefab => _canvasPrefab;

        public GameObject CameraPrefab => _cameraPrefab;

        public PanelPrefabLink[] PanelPrefabLinks => _panelPrefabLinks;

        public PanelPrefabLink[] SubpanelPrefabLinks => _subpanelPrefabLinks;

        public GameObject GetPanelPrefab(string panelName)
        {
            foreach (PanelPrefabLink link in _panelPrefabLinks)
            {
                if (link.Id == panelName)
                {
                    return link.PrefabLink.Load();
                }
            }
            
            Debug.LogError("Not found panel with id: " + panelName);

            return null;
        }

        public GameObject GetSubpanelPrefab(string panelName)
        {
            foreach (PanelPrefabLink link in _subpanelPrefabLinks)
            {
                if (link.Id == panelName)
                {
                    return link.PrefabLink.Load();
                }
            }
            
            Debug.LogError("Not found subpanel with id: " + panelName);

            return null;
        }
    }
}