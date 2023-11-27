using Client.Scripts.Tools.Services;

namespace Client.Scripts.EmptyGame.Systems
{
    public class StartGameSystem : IBattleSystem
    {
        public void Start()
        {
            Service.BoardService.SpawnComponent.SpawnPiece();
        }

        public void Stop()
        {
        }

        public void OnUpdate()
        {
        }
    }
}