using UnityEngine;

namespace Client.Scripts.Ui.Bind
{
    public class AnimatorBind : IBind
    {
        private Animator _animator;

        public bool Init(GameObject gameObject)
        {
            _animator = gameObject.GetComponent<Animator>();

            return _animator;
        }

        public void SetTrigger(int id)
        {
            _animator.SetTrigger(id);
        }

        public void SetFloat(int id, float value)
        {
            _animator.SetFloat(id, value);
        }
    }
}