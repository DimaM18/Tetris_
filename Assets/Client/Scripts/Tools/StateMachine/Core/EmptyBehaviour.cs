using System;

using UnityEngine;

namespace Client.Scripts.Tools.StateMachine.Core
{
    public class EmptyBehaviour : StateMachineBehaviour
    {
        public event Action<string> Started;
        public event Action<string> Finished;

        [SerializeField]
        private string _name;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Started?.Invoke(_name);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Finished?.Invoke(_name);
        }
    }
}