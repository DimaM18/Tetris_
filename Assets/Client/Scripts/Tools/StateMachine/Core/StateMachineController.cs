using System.Collections.Generic;

using UnityEngine;

namespace Client.Scripts.Tools.StateMachine.Core
{
    public abstract class StateMachineController
    {
        protected Dictionary<string, IState> States;
        protected IState CurrentState;
        protected readonly Animator Animator;

        private bool _isInit;
        
        private readonly GameObject _animatorObject;
        private readonly EmptyBehaviour[] _behaviours;
        
        private static readonly int BreakStateTrigger = Animator.StringToHash("BreakState");

        protected StateMachineController(GameObject prototype)
        {
            _animatorObject = Object.Instantiate(prototype);
            Animator = _animatorObject.GetComponent<Animator>();
            _behaviours = Animator.GetBehaviours<EmptyBehaviour>();

            foreach (EmptyBehaviour behaviour in _behaviours)
            {
                behaviour.Started += OnStateStarted;
                behaviour.Finished += OnStateFinished;
            }
        }

        public void DeInit()
        {
            foreach (EmptyBehaviour behaviour in _behaviours)
            {
                behaviour.Started -= OnStateStarted;
                behaviour.Finished -= OnStateFinished;
            }
            
            CurrentState?.DeInit();
            
            Object.Destroy(_animatorObject);
        }

        protected abstract void InitStates();

        private void OnStateStarted(string stateName)
        {
            if (_isInit == false)
            {
                InitStates();
                _isInit = true;
            }

            if (States.ContainsKey(stateName))
            {
                CurrentState = States[stateName];
                CurrentState.Init();
            }
            else
            {
                Debug.LogError("Unknown state in state machine: " + stateName);
            }
        }

        private void OnStateFinished(string stateName)
        {
            if (States.ContainsKey(stateName))
            {
                IState finishedState = States[stateName];
                finishedState.DeInit();

                if (finishedState == CurrentState)
                {
                    CurrentState = null;
                }
            }
            else
            {
                Debug.LogError("Unknown state in state machine: " + stateName);
            }
        }

        protected void BreakState()
        {
            Animator.SetTrigger(BreakStateTrigger);
        }
    }
}