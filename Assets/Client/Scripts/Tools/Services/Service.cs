using System;
using System.Collections.Generic;

using Client.Scripts.Audio;
using Client.Scripts.Ui;

namespace Client.Scripts.Tools.Services
{
    public class Service
    {
        private static Service _instance;
        
        private readonly Dictionary<Type, IService> _services = new();

        public static Service Create()
        {
            if (_instance != null)
            {
                _instance.DeInit();
            }
            
            _instance = new Service();

            return _instance;
        }
        
        private Service()
        {
        }
        
        public void OnUpdate()
        {
            foreach (KeyValuePair<Type,IService> service in _services)
            {
                service.Value.OnUpdate();
            }
        }

        public void DeInit()
        {
            foreach (KeyValuePair<Type,IService> service in _services)
            {
                service.Value.DeInit();
            }
            
            _services.Clear();
            _instance = null;
        }
        
        public static void Register(IService service)
        {
            Type type = service.GetType();

            if (_instance._services.ContainsKey(type))
            {
                _instance._services[type] = service;
            }
            else
            {
                _instance._services.Add(type, service);
            }
        }

        public static T Get<T>() where T : class, IService
        {
            return _instance._services[typeof(T)] as T;
        }

        public static Pool.Pool Pool => Get<Pool.Pool>();
        public static UiManager UiManager => Get<UiManager>();
        public static BoardService.BoardService BoardService => Get<BoardService.BoardService>();
        public static Timer Timer => Get<Timer>();
        public static GlobalEventDispatcher GlobalEvent => Get<GlobalEventDispatcher>();
        public static AudioManager AudioManager => Get<AudioManager>();
        public static SequenceService Sequence => Get<SequenceService>();
    }
}