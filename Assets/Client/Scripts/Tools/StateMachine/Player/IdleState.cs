using Client.Scripts.Tools.StateMachine.Core;

using UnityEngine;

namespace Client.Scripts.Tools.StateMachine.Player
{
    public class IdleState : IState
    {
        public void Init()
        {
            Debug.Log("Start idle animation");
        }

        public void DeInit()
        {
            Debug.Log("Finish idle animation");
        }
    }
}