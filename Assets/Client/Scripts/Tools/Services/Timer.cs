using System;
using System.Collections.Generic;

using UnityEngine;


namespace Client.Scripts.Tools.Services
{
    public class Timer : IService
    {
        private class DelayAction
        {
            public bool IsEmpty;
            public Action Action;
            public float Time;
        }
        
        private class IntervalAction
        {
            public bool IsEmpty;
            public Action Action;
            public float Time;
            public float Interval;
        }
        
        private class UpdateAction
        {
            public bool IsEmpty;
            public Action Action;
        }
        
        private readonly List<UpdateAction> _updateActions = new();
        private readonly List<DelayAction> _delayActions = new();
        private readonly List<IntervalAction> _intervalActions = new();
        
        public void OnUpdate()
        {
            int countActions = _updateActions.Count;
            for (int i = 0; i < countActions; i++)
            {
                UpdateAction action = _updateActions[i];
                if (action.IsEmpty)
                {
                    continue;
                }

                action.Action.Invoke();
            }

            float deltaTime = Time.deltaTime;
            
            countActions = _delayActions.Count;
            for (int i = 0; i < countActions; i++)
            {
                DelayAction action = _delayActions[i];
                if (action.IsEmpty)
                {
                    continue;
                }

                action.Time -= deltaTime;

                if (action.Time <= 0.0f)
                {
                    action.Action.Invoke();
                    action.Action = null;
                    action.IsEmpty = true;
                }
            }

            countActions = _intervalActions.Count;
            for (int i = 0; i < countActions; i++)
            {
                IntervalAction action = _intervalActions[i];
                if (action.IsEmpty)
                {
                    continue;
                }

                action.Time -= deltaTime;

                if (action.Time <= 0.0f)
                {
                    action.Action.Invoke();
                    action.Time += action.Interval;
                }
            }
        }

        public void DeInit()
        {
            _updateActions.Clear();
        }

        public void AddUpdateListener(Action action)
        {
            foreach (UpdateAction updateAction in _updateActions)
            {
                if (updateAction.IsEmpty)
                {
                    updateAction.Action = action;
                    updateAction.IsEmpty = false;
                    return;
                }
            }
            
            _updateActions.Add(new UpdateAction { Action = action, IsEmpty = false });
        }
        
        public void RemoveUpdateListener(Action action)
        {
            foreach (UpdateAction updateAction in _updateActions)
            {
                if (updateAction.Action == action)
                {
                    updateAction.Action = null;
                    updateAction.IsEmpty = true;
                }
            }
        }

        public void AddDelayListener(Action action, float delay)
        {
            foreach (DelayAction delayAction in _delayActions)
            {
                if (delayAction.IsEmpty)
                {
                    delayAction.Action = action;
                    delayAction.Time = delay;
                    delayAction.IsEmpty = false;
                    return;
                }
            }

            _delayActions.Add(new DelayAction { Action = action, Time = delay, IsEmpty = false });
        }
        
        public void RemoveDelayListener(Action action)
        {
            foreach (DelayAction delayAction in _delayActions)
            {
                if (delayAction.Action == action)
                {
                    delayAction.Action = null;
                    delayAction.IsEmpty = true;
                }
            }
        }
        
        public void AddIntervalListener(Action action, float interval)
        {
            foreach (IntervalAction intervalAction in _intervalActions)
            {
                if (intervalAction.IsEmpty)
                {
                    intervalAction.Action = action;
                    intervalAction.Time = interval;
                    intervalAction.Interval = interval;
                    intervalAction.IsEmpty = false;
                    return;
                }
            }

            _intervalActions.Add(new IntervalAction { Action = action, Time = interval, Interval = interval, IsEmpty = false });
        }
        
        public void RemoveIntervalListener(Action action)
        {
            foreach (IntervalAction intervalAction in _intervalActions)
            {
                if (intervalAction.Action == action)
                {
                    intervalAction.Action = null;
                    intervalAction.IsEmpty = true;
                }
            }
        }
    }
}