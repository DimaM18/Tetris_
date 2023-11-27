using Client.Scripts.Tools.PlayerPrefsWrapper;

namespace Client.Scripts.DataStorage
{
    public class Settings
    {
        public readonly PlayerPrefsBool MusicEnabled = new("Settings.MusicEnabled", true);
        public readonly PlayerPrefsBool SoundEnabled = new("Settings.SoundEnabled", true);
    }
}