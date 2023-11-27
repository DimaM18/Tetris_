using UnityEngine;
using UnityEngine.UI;

namespace Client.Scripts.Ui.Bind
{
    public class ImageBind : IBind
    {
        public Sprite Sprite
        {
            set => _image.sprite = value;
        }
        
        private Image _image;
        
        public bool Init(GameObject gameObject)
        {
            _image = gameObject.GetComponent<Image>();

            return _image;
        }
    }
}