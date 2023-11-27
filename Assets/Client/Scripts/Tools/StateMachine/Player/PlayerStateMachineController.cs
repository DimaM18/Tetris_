using System.Collections.Generic;

using Client.Scripts.Tools.StateMachine.Core;

using UnityEngine;

namespace Client.Scripts.Tools.StateMachine.Player
{
    public class PlayerStateMachineController : StateMachineController
    {
        private static readonly int RunProperty = Animator.StringToHash("Run");
        private static readonly int JumpTrigger = Animator.StringToHash("Jump");

        public PlayerStateMachineController(GameObject prototype) : base(prototype)
        {
        }

        protected override void InitStates()
        {
            States = new Dictionary<string, IState>
            {
                {"Run", new RunState()},
                {"Idle", new IdleState()},
                {"Jump", new JumpState(BreakState)}
            };
        }

        public void Run()
        {
            Animator.SetBool(RunProperty, true);
        }

        public void Idle()
        {
            Animator.SetBool(RunProperty, false);
        }

        public void Jump()
        {
            if (CurrentState is JumpState)
            {
                return;
            }
            
            Animator.SetTrigger(JumpTrigger);
        }
    }
}