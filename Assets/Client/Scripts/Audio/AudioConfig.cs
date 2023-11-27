using UnityEngine;
using UnityEngine.Audio;


namespace Client.Scripts.Audio
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        [SerializeField]
        private AudioMixer _masterMixer;

        [SerializeField]
        private string _musicVolumeTag;

        [SerializeField]
        private string _soundVolumeTag;

        [SerializeField]
        private float _muteValue;
        
        public AudioMixer MasterMixer => _masterMixer;
        public string MusicVolumeTag => _musicVolumeTag;
        public string SoundVolumeTag=> _soundVolumeTag;
        public float MuteValue => _muteValue;
    }
}