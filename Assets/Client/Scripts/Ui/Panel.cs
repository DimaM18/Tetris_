using System;
using System.Collections.Generic;

using Client.Scripts.Ui.Bind;

using UnityEngine;

namespace Client.Scripts.Ui
{
    public class Panel : MonoBehaviour
    {
        [Serializable]
        private class ObjectLink
        {
            public string Name;
            public GameObject Object;
        }

        [SerializeField]
        private ObjectLink[] _links;

        private readonly Dictionary<string, GameObject> _dictionary = new();

        private void Awake()
        {
            foreach (ObjectLink link in _links)
            {
                _dictionary.Add(link.Name, link.Object);
            }
        }

        public T Bind<T>(string bindName) where T : class, IBind, new()
        {
            if (_dictionary.ContainsKey(bindName) == false)
            {
                Debug.LogError("Panel " + name + " not contains bind name: " + bindName);
                return null;
            }

            T bind = new();
            bool isCorrectBinding = bind.Init(_dictionary[bindName]);

            if (isCorrectBinding == false)
            {
                Debug.LogError("Panel " + name + " incorrect binding: " + bindName + " to type " + typeof(T));
            }

            return bind;
        }
    }
}