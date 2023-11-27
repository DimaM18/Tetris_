using UnityEngine;

namespace Client.Scripts.Ui.Bind
{
    public class GameObjectBind : IBind
    {
        public GameObject GameObject { get; private set; }

        public bool Init(GameObject gameObject)
        {
            GameObject = gameObject;

            return true;
        }
    }
}