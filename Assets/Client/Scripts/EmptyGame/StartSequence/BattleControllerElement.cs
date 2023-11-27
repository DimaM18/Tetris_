using System;
using System.Collections.Generic;

using Client.Scripts.Game;


namespace Client.Scripts.EmptyGame.StartSequence
{
    public class BattleControllerElement : ILevelSequenceElement
    {
        public event Action Finished;
        
        public void Start(Dictionary<string, object> context)
        {
            context.Add(GameSequenceTags.GameController, new TetrisGameController());

            Finished?.Invoke();
        }
    }
}