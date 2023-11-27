using System;
using System.Collections.Generic;

using Client.Scripts.DataStorage;
using Client.Scripts.Game;


namespace Client.Scripts.EmptyGame.StartSequence
{
    public class BattleReadyElement : ILevelSequenceElement
    {
        public event Action Finished;
        
        public void Start(Dictionary<string, object> context)
        {
            Data.Game.GameReady?.Invoke((IGameController)context[GameSequenceTags.GameController]);
            
            Finished?.Invoke();
        }
    }
}