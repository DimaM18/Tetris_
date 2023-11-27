using System.Collections.Generic;

using Client.Scripts.DataStorage;
using Client.Scripts.Tools.Services;

using UnityEngine;


namespace Client.Scripts.Audio
{
    public class AudioManager : MonoBehaviour, IService
    {
        private AudioSource _musicSource;
        private Stack<AudioSource> _freeSoundSources;
        private Queue<AudioSource> _busySoundSources;

        private SoundEffect _currMusic;
        private Dictionary<string, SoundEffect> _effects;

        private static AudioManager _instance;

        private readonly Dictionary<AudioSource, SoundEffect> _sourceEffectLink = new();

        public static AudioManager Create()
        {
            if (_instance)
            {
                return _instance;
            }
            
            var audioObject = new GameObject("AudioManager");
            return audioObject.AddComponent<AudioManager>();
        }
        
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                Init();
            }
        }

        private void Init()
        {
            DontDestroyOnLoad(gameObject);
            
            _freeSoundSources = new Stack<AudioSource>();
            _busySoundSources = new Queue<AudioSource>();
            _effects = new Dictionary<string, SoundEffect>();

            _musicSource = gameObject.AddComponent<AudioSource>();
            _freeSoundSources.Push(gameObject.AddComponent<AudioSource>());

            PreloadSounds();
        }

        public void DeInit()
        {
            
        }

        public void OnUpdate()
        {
            int countBusySources = _busySoundSources.Count;

            for (int i = 0; i < countBusySources; i++)
            {
                AudioSource source = _busySoundSources.Dequeue();
                if (source.isPlaying)
                {
                    _busySoundSources.Enqueue(source);
                }
                else
                {
                    FreeSource(source);
                }
            }
        }

        private void PreloadSounds()
        {
            GetSoundEffect(Sounds.ButtonClick);
        }
        
        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                int countBusySources = _busySoundSources.Count;

                for (int i = 0; i < countBusySources; i++)
                {
                    AudioSource source = _busySoundSources.Dequeue();
                    if (source.isPlaying)
                    {
                        source.Stop();
                        source.clip = null;
                    }
                    
                    FreeSource(source);
                }
            }
        }

        private void FreeSource(AudioSource source)
        {
            if (_sourceEffectLink.ContainsKey(source))
            {
                _sourceEffectLink[source].FreeOne();
                _sourceEffectLink.Remove(source);
            }
            
            _freeSoundSources.Push(source);
        }

        public void Play(string effectName)
        {
            Play(effectName, false);
        }
        
        public void Play(string effectName, int index)
        {
            Play(effectName, true, index);
        }
        
        private void Play(string effectName, bool withIndex, int index = 0)
        {
            SoundEffect effect = GetSoundEffect(effectName);

            if (effect.IsCanPlay() == false)
            {
                return;
            }

            if (effect.IsMusic)
            {
                ChangeMusic(effect);
            }
            else
            {
                AudioSource source = GetSoundAudioSource();
                _sourceEffectLink.Add(source, effect);
                
                if (withIndex)
                {
                    effect.Play(source, index);
                }
                else
                {
                    effect.Play(source);
                }
                _busySoundSources.Enqueue(source);
            }
        }

        private void ChangeMusic(SoundEffect musicEffect)
        {
            if (musicEffect != _currMusic)
            {
                _musicSource.Stop();
                _currMusic = musicEffect;
                if (musicEffect)
                {
                    musicEffect.Play(_musicSource);
                }
            }
        }

        public void EnableMusic(bool isEnable)
        {
          //  AudioConfig config = Data.Configs.Audio;
           // config.MasterMixer.SetFloat(config.MusicVolumeTag, isEnable ? 0.0f : config.MuteValue);
        }

        public void EnableSounds(bool isEnable)
        {
           // AudioConfig config = Data.Configs.Audio;
           // config.MasterMixer.SetFloat(config.SoundVolumeTag, isEnable ? 0.0f : config.MuteValue);
        }

        private AudioSource GetSoundAudioSource()
        {
            if (_freeSoundSources.Count > 0)
            {
                return _freeSoundSources.Pop();
            }

            return gameObject.AddComponent<AudioSource>();
        }

        private SoundEffect GetSoundEffect(string effectName)
        {
            if (_effects.ContainsKey(effectName))
            {
                return _effects[effectName];
            }

            SoundEffect effect = Resources.Load<SoundEffect>("Audio/SoundEffects/" + effectName);
            _effects.Add(effectName, effect);

            return effect;
        }
    }
}