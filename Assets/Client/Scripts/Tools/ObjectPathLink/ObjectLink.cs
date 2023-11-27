using System;

using UnityEngine;

using Object = UnityEngine.Object;


namespace Client.Scripts.Tools.ObjectPathLink
{
    [Serializable]
    public class GameObjectLink : ObjectLink<GameObject>
    {
        public GameObjectLink(string path) : base(path)
        {}
        
        public GameObjectLink()
        {}
    }
    
    [Serializable]
    public class TextAssetLink : ObjectLink<TextAsset>
    {
        public TextAssetLink(string path) : base(path)
        {}
        
        public TextAssetLink()
        {}
    }
    
    [Serializable]
    public class MaterialLink : ObjectLink<Material>
    {
        public MaterialLink(string path) : base(path)
        {}
        
        public MaterialLink()
        {}
    }

    [Serializable]
    public class TextureLink : ObjectLink<Texture>
    {
        public TextureLink(string path) : base(path)
        { }

        public TextureLink()
        {
        }
    }
    
    [Serializable]
    public class ObjectLink<T> where T : Object
    {
        [SerializeField]
        [AssetPath.Attribute(typeof(Object))]
        private string _path;

        public ObjectLink(string path)
        {
            _path = path;
        }
        
        public ObjectLink()
        {}

        public T Load()
        {
            Debug.Assert(_path.Contains("Resources"), "Path " + _path + " is not in resource folder!!!");
            return AssetPath.Load<T>(_path);
        }
        
        public T LoadInEditor()
        {
            return AssetPath.Load<T>(_path);
        }

        public string GetName()
        {
            int dotIndex = _path.LastIndexOf('.');
            int slashIndex = _path.LastIndexOf('/');
            
            return _path.Substring(slashIndex + 1, dotIndex - slashIndex - 1);
        }

        public bool HasName()
        {
            return !string.IsNullOrEmpty(_path);
        }

        public static bool operator ==(ObjectLink<T> self, ObjectLink<T> other)
        {
            return Equals(self, other);
        }

        public static bool operator !=(ObjectLink<T> self, ObjectLink<T> other)
        {
            return !Equals(self, other);
        }
        
        protected bool Equals(ObjectLink<T> other)
        {
            return _path == other._path;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((ObjectLink<T>) obj);
        }

        public override int GetHashCode()
        {
            return _path != null ? _path.GetHashCode() : 0;
        }
    }
}