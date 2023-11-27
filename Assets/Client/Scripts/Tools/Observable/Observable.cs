using System;

namespace Client.Scripts.Tools.Observable
{
    [Serializable]
    public class Observable<T> where T : IEquatable<T>
    {
        public event Action<T, T> Changed;
        
        private T _value;
        
        public T Value
        {
            get => _value;
            set
            {
                if (value.Equals(_value))
                {
                    return;
                }
                
                T oldValue = _value;
                _value = value;
                Changed?.Invoke(oldValue, value);
            }
        }
 
        public Observable()
        {
        }
 
        public Observable(T value)
        {
            _value = value;
        }
 
        public override string ToString()
        {
            return _value.ToString();
        }
    }
}