using Client.Scripts.EmptyGame.StartSequence;
using Client.Scripts.Game;


namespace Client.Scripts.EmptyGame
{
    public class TetrisInitGameSequence : GameSequence
    {
        public TetrisInitGameSequence()
        {
            Elements.Add(new StartScreenElement());
            Elements.Add(new MainScreenElement());
            Elements.Add(new BattleControllerElement());
            Elements.Add(new BattleReadyElement());
        }
    }
}