using System;
using System.Collections.Generic;


namespace Client.Scripts.Game
{
    public interface ILevelSequenceElement
    {
        event Action Finished;

        void Start(Dictionary<string, object> context);
    }
}