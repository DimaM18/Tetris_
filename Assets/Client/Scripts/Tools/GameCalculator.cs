using Client.Scripts.DataStorage;

namespace Client.Scripts.Tools
{
    public static class GameCalculator
    {
        //https://tetris.fandom.com/wiki/Scoring
        // 1 lines 	40 * (n + 1)	2 lines 100 * (n + 1)	3 lines 300 * (n + 1)	4 lines 1200 * (n + 1)
        public static int GetScoreToAdd(int lines, int currentLevel)
        {
            int multiplier = 40;
            switch (lines)
            {
                default:
                    multiplier = 40;
                    break;
                case 2 :
                    multiplier = 100;
                    break;
                case 3 :
                    multiplier = 300;
                    break;
                case 4 :
                    multiplier = 1200;
                    break;
            }
            
            return multiplier * (currentLevel + 1);
        }
        
        public static int GetCurrentLevel()
        {
            return Data.Game.Empty.LinesCount.Value / Data.Configs.DifficultyConfig.LinesToLevelUp;
        }
    }
}