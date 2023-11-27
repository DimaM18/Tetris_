using UnityEngine;

namespace Client.Scripts.Tools.PlayerPrefsWrapper
{
    public class PlayerPrefsInt : PlayerPrefsValue<int>
    {
        public PlayerPrefsInt(string key, int defaultValue) : base(key)
        {
            RealtimeValue = PlayerPrefs.GetInt(Key, defaultValue);
        }

        protected override void SaveToStorage(int value)
        {
            PlayerPrefs.SetInt(Key, value);
        }
    }
}