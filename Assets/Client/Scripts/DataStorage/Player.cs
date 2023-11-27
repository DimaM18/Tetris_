using Client.Scripts.Tools.PlayerPrefsWrapper;

namespace Client.Scripts.DataStorage
{
    public class Player
    {
        public readonly PlayerPrefsInt Score = new("Player.Score", 0);
    }
}