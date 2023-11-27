using UnityEngine;

namespace Client.Scripts.Tools.StateMachine.Core
{
    [CreateAssetMenu(fileName = "StateMachineConfig", menuName = "Configs/StateMachineConfig")]
    public class StateMachineConfig : ScriptableObject
    {
        [SerializeField]
        private GameObject _playerAnimator;

        public GameObject PlayerAnimator => _playerAnimator;
    }
}