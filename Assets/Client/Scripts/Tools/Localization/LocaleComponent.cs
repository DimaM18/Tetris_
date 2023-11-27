using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Client.Scripts.Tools.Localization
{
    public class LocaleComponent : MonoBehaviour
    {
        [SerializeField]
        private string _id = "";

        private Text _text;
        private TMP_Text _tmpText;

        private void OnEnable()
        {
            _text = GetComponent<Text>();
            _tmpText = GetComponent<TMP_Text>();
            Localise();
        }

        private void Localise()
        {
            if (string.IsNullOrEmpty(_id))
            {
                return;
            }

            string localisedString = Locale.GetLocalisedString(_id);
            if (!string.IsNullOrEmpty(localisedString))
            {
                if (_text != null)
                {
                    _text.text = localisedString;    
                }

                if (_tmpText != null)
                {
                    _tmpText.text = localisedString;
                }
            }
        }
    }
}