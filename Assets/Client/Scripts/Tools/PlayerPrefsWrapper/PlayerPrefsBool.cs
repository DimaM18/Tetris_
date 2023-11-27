using UnityEngine;

namespace Client.Scripts.Tools.PlayerPrefsWrapper
{
    public class PlayerPrefsBool : PlayerPrefsValue<bool>
    {
        public PlayerPrefsBool(string key, bool defaultValue) : base(key)
        {
            RealtimeValue = PlayerPrefs.GetInt(Key, defaultValue ? 1 : 0) == 1;
        }

        protected override void SaveToStorage(bool value)
        {
            PlayerPrefs.SetInt(Key, value ? 1 : 0);
        }
    }
}