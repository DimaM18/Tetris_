using System;
using System.Collections.Generic;


namespace Client.Scripts.Game
{
    public abstract class GameSequence
    {
        private readonly Dictionary<string, object> _context = new();
        protected readonly List<ILevelSequenceElement> Elements = new();
        private int _currentElement;

        public event Action Finished;
        public void Start()
        {
            _currentElement = -1;
            Next();
        }

        private void Next()
        {
            _currentElement++;

            if (_currentElement < Elements.Count)
            {
                Elements[_currentElement].Finished += OnElementFinished;
                Elements[_currentElement].Start(_context);
            }
            else
            {
                Finish();
            }
        }

        private void Finish()
        {
            Finished?.Invoke();
            _context.Clear();
            Elements.Clear();
        }

        private void OnElementFinished()
        {
            Elements[_currentElement].Finished -= OnElementFinished;
            Next();
        }
    }
}