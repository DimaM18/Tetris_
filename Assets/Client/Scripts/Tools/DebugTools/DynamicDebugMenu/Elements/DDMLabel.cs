using UnityEngine;
using UnityEngine.UI;


namespace Client.Scripts.Tools.DebugTools.DynamicDebugMenu.Elements
{
    public class DDMLabel : MonoBehaviour
    {
        [SerializeField]
        private Text _label;
        
        public static void Create(DDMContext context, string text)
        {
            GameObject labelObject = Instantiate(context.Config.LabelPrefab, context.Parent);
            DDMLabel label = labelObject.GetComponent<DDMLabel>();

            label._label.text = text;
        }
    }
}