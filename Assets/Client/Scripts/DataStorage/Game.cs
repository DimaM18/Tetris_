using System;
using Client.Scripts.Game;


namespace Client.Scripts.DataStorage
{
    public class Game
    {
        public Action<IGameController> GameReady;
        public readonly GameData.GameData Empty = new();
    }
}