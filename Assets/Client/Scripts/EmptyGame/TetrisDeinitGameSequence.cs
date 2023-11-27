using Client.Scripts.EmptyGame.LoseSequence;
using Client.Scripts.Game;


namespace Client.Scripts.EmptyGame
{
    public class TetrisDeinitGameSequence : GameSequence
    {
        public TetrisDeinitGameSequence()
        {
            Elements.Add(new LoadGameLoseScreen());
            Elements.Add(new ReloadGameElement());
        }
    }
}