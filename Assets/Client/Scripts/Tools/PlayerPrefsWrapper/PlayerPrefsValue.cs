using System;

namespace Client.Scripts.Tools.PlayerPrefsWrapper
{
    [Serializable]
    public abstract class PlayerPrefsValue<T>
    {
        protected readonly string Key;
        protected T RealtimeValue;

        protected PlayerPrefsValue(string key)
        {
            Key = key;
        }

        public T Value
        {
            get => RealtimeValue;
            set
            {
                if (value.Equals(RealtimeValue))
                {
                    return;
                }
                
                RealtimeValue = value;
                SaveToStorage(value);
            }
        }
        
        protected abstract void SaveToStorage(T value);
    }
}