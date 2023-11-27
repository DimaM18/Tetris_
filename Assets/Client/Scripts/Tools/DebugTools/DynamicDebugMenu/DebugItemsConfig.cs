using UnityEngine;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu
{
    [CreateAssetMenu(fileName = "DebugItemsConfig", menuName = "Configs/DebugItemsConfig")]
    public class DebugItemsConfig: ScriptableObject
    {
        public GameObject MainPrefab;
        public GameObject PagePrefab;
        public GameObject SubmenuPrefab;
        public GameObject LabelPrefab;
        public GameObject CheckboxPrefab;
        public GameObject InputPrefab;
        public GameObject DropdownPrefab;
        public GameObject DropdownSavePrefab;
        public GameObject ButtonPrefab;
        public GameObject InputButtonPrefab;
        public GameObject CheckboxSavePrefab;
    }
}