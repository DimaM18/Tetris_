namespace Client.Scripts.DataStorage
{
    public class Data
    {
        private readonly Configs _configs = new();
        private readonly SceneLinks _sceneLinks = new();
        private readonly Meta _meta = new();
        private readonly Game _game = new();

        private static Data _instance;

        public static Configs Configs => _instance._configs;
        public static Meta Meta => _instance._meta;
        public static Game Game => _instance._game;
        public static SceneLinks SceneLinks => _instance._sceneLinks;

        private Data()
        {
            
        }

        public static void Create()
        {
            _instance ??= new Data();
        }
    }
}