using UnityEngine;

namespace Client.Scripts.Tools.PlayerPrefsWrapper
{
    public class PlayerPrefsFloat : PlayerPrefsValue<float>
    {
        public PlayerPrefsFloat(string key, float defaultValue) : base(key)
        {
            RealtimeValue = PlayerPrefs.GetFloat(Key, defaultValue);
        }

        protected override void SaveToStorage(float value)
        {
            PlayerPrefs.SetFloat(Key, value);
        }
    }
}