using UnityEngine;

namespace Client.Scripts.Tools.PlayerPrefsWrapper
{
    public class PlayerPrefsString : PlayerPrefsValue<string>
    {
        public PlayerPrefsString(string key, string defaultValue) : base(key)
        {
            RealtimeValue = PlayerPrefs.GetString(Key, defaultValue);
        }

        protected override void SaveToStorage(string value)
        {
            PlayerPrefs.SetString(Key, value);
        }
    }
}