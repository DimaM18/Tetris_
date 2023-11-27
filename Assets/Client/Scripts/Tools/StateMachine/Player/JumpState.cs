using System;

using Client.Scripts.Tools.Services;
using Client.Scripts.Tools.StateMachine.Core;

using UnityEngine;

namespace Client.Scripts.Tools.StateMachine.Player
{
    public class JumpState : IState
    {
        private readonly Action _breakAction;

        public JumpState(Action breakAction)
        {
            _breakAction = breakAction;
        }
        
        public void Init()
        {
            Debug.Log("Start jump animation");
            
            Service.Timer.AddDelayListener(OnJumpEnd, 2.0f);
        }

        public void DeInit()
        {
            Debug.Log("Finish jump animation");

            Service.Timer.RemoveDelayListener(OnJumpEnd);
        }

        private void OnJumpEnd()
        {
            Debug.Log("Break jump");
            _breakAction?.Invoke();
        }
    }
}