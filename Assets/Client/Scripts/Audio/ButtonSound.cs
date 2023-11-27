using Client.Scripts.Tools.Services;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Client.Scripts.Audio
{
    [RequireComponent(typeof(Button))]
    public class ButtonSound : MonoBehaviour, IPointerDownHandler
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_button.interactable)
            {
                Service.AudioManager.Play(Sounds.ButtonClick);
            }
        }
    }
}