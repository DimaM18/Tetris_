namespace Client.Scripts.Tools.StateMachine.Core
{
    public interface IState
    {
        public void Init();
        public void DeInit();
    }
}