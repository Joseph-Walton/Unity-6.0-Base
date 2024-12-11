using UnityEngine;

namespace ComponentStateMachine
{
    public abstract class State : MonoBehaviour
    {
        public bool IsComplete { get; private set; }

        public virtual void Enter() { }
        public virtual void Do() { }
        public virtual void FixedDo() { }
        public virtual void Exit() { }
    }
}

