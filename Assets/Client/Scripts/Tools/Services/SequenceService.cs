using System;
using System.Collections.Generic;

using Client.Scripts.EmptyGame;
using Client.Scripts.Game;
using Client.Scripts.Tools.Enums;

using UnityEngine;


namespace Client.Scripts.Tools.Services
{
    public class SequenceService : IService
    {
        private class SequenceCell
        {
            public readonly SequenceType SequenceType;
            public readonly Func<bool> ActivationFunction;
            public readonly Func<GameSequence> SequenceCreationFunction;

            public SequenceCell(SequenceType sequenceType, Func<bool> activationFunction, Func<GameSequence> sequenceCreationFunction)
            {
                SequenceType = sequenceType;
                ActivationFunction = activationFunction;
                SequenceCreationFunction = sequenceCreationFunction;
            }

            public SequenceCell(SequenceType sequenceType, Func<bool> activationFunction, Type sequenceConstructionType)
            {
                SequenceType = sequenceType;
                ActivationFunction = activationFunction;
                SequenceCreationFunction = () => (GameSequence) Activator.CreateInstance(sequenceConstructionType);
            }

            public SequenceCell(SequenceType sequenceType, Type sequenceConstructionType)
            {
                SequenceType = sequenceType;
                ActivationFunction = () => true;
                SequenceCreationFunction = () => (GameSequence) Activator.CreateInstance(sequenceConstructionType);
            }
        }

        private readonly SequenceCell[] _sequenceTable =
        {
            new(SequenceType.Init, typeof(TetrisInitGameSequence)),
            new(SequenceType.Win, typeof(TetrisDeinitGameSequence)),
            new(SequenceType.Lose, typeof(TetrisDeinitGameSequence)),
        };

        private readonly List<SequenceCell> _overridesTable = new();

        private GameSequence _currentRunningSequence;
        

        public void StartSequence(SequenceType sequenceType)
        {
            if (_currentRunningSequence != null)
            {
                Debug.LogError("Can't start sequence while other sequence is running");
                return;
            }

            bool overrideSequenceStarted = TryStartSequence(sequenceType, _overridesTable);
            if (overrideSequenceStarted)
            {
                return;
            }
            
            bool defaultSequenceStarted = TryStartSequence(sequenceType, _sequenceTable);
            if (defaultSequenceStarted)
            {
                return;
            }
            
            Debug.LogError($"Can't find sequence cell for sequence type: {sequenceType}");
        }

        private bool TryStartSequence(SequenceType type, IEnumerable<SequenceCell> collection)
        {
            foreach (SequenceCell sequenceCell in collection)
            {
                if (sequenceCell.SequenceType == type && sequenceCell.ActivationFunction())
                {
                    _currentRunningSequence = sequenceCell.SequenceCreationFunction();
                    _currentRunningSequence.Finished += OnSequenceFinished;
                    _currentRunningSequence.Start();
                    return true;
                }
            }

            return false;
        }

        private void OnSequenceFinished()
        {
            _currentRunningSequence.Finished -= OnSequenceFinished;
            _currentRunningSequence = null;
        }

        public void OnUpdate()
        {
            
        }

        public void DeInit()
        {
            
        }

        public void OverrideSequence(SequenceType type, Type sequenceConstructionType)
        {
            _overridesTable.Add(new SequenceCell(type, sequenceConstructionType));
        }
    }
}