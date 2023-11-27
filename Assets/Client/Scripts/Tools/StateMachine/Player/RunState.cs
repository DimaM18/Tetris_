using Client.Scripts.Tools.StateMachine.Core;

using UnityEngine;

namespace Client.Scripts.Tools.StateMachine.Player
{
    public class RunState : IState
    {
        public void Init()
        {
            Debug.Log("Start run animation");
        }

        public void DeInit()
        {
            Debug.Log("Finish run animation");
        }
    }
}