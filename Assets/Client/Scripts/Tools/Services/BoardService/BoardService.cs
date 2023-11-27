namespace Client.Scripts.Tools.Services.BoardService
{
    public class BoardService : IService
    {
        private MapComponent _mapComponent;
        private LinesComponent _linesComponent;
        private SpawnComponent _spawnComponent;

        public MapComponent MapComponent => _mapComponent;
        public LinesComponent LinesComponent => _linesComponent;
        public SpawnComponent SpawnComponent => _spawnComponent;

        public BoardService()
        {
            _mapComponent = new MapComponent();
            _linesComponent = new LinesComponent();
            _spawnComponent = new SpawnComponent();
        }
        
        public void OnUpdate()
        {
        }

        public void DeInit()
        {
        }
        
        
    }
}