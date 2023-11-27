using System;
using System.Collections.Generic;

using Client.Scripts.Tools.Services.GlobalEvents;

namespace Client.Scripts.Tools.Services
{
    public class GlobalEventDispatcher : IService
    {
        private class Data
        {
            public bool IsEmpty;
            public Delegate Action;
        }
        
        private readonly Dictionary<Type, List<Data>> _delegates = new();

        public void OnUpdate()
        {
        }

        public void DeInit()
        {
            _delegates.Clear();
        }

        public void AddListener<T>(Action<T> listener) where T : GlobalEvent
        {
            Type type = typeof(T);
            List<Data> datas;
            
            if (_delegates.TryGetValue(type, out datas))
            {
                Data newData = null;
                foreach (Data data in datas)
                {
                    if (data.IsEmpty)
                    {
                        newData = data;
                        break;
                    }
                }

                if (newData == null)
                {
                    newData = new Data();
                    datas.Add(newData);
                }

                newData.IsEmpty = false;
                newData.Action = listener;
            }
            else
            {
                var newData = new Data
                {
                    IsEmpty = false,
                    Action = listener
                };

                datas = new List<Data> {newData};
                _delegates.Add(type, datas);
            }
        }

        public void RemoveListener<T>(Action<T> listener) where T : GlobalEvent
        {
            Type type = typeof(T);
            Delegate action = listener;
            
            if (_delegates.TryGetValue(type, out List<Data> datas))
            {
                foreach (Data data in datas)
                {
                    if (data.IsEmpty == false && data.Action == action)
                    {
                        data.IsEmpty = true;
                        data.Action = null;
                    }
                }
            }
        }

        public void Dispatch<T>(T globalEvent) where T : GlobalEvent
        {
            Type type = typeof(T);
            
            if (_delegates.TryGetValue(type, out List<Data> datas))
            {
                for (int i = 0; i < datas.Count; i++)
                {
                    Data data = datas[i];
                    if (data.IsEmpty == false)
                    {
                        Action<T> callback = data.Action as Action<T>;
                        callback?.Invoke(globalEvent);
                    }
                }
            }
        }
    }
}