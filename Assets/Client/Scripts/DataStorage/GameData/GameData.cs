using Client.Scripts.Tools.Observable;

namespace Client.Scripts.DataStorage.GameData
{
    public class GameData
    {
        public Observable<int> LinesCount;
        public Observable<int> Level;
        public Observable<int> Score;
        public Observable<int> NextTetrominoIndex;
        public Observable<bool> Paused;

        public void Reset()
        {
            LinesCount = new(0);
            Level = new(0);
            Score = new(0);
            NextTetrominoIndex = new(0);
            Paused = new(false);
        }
    }
}