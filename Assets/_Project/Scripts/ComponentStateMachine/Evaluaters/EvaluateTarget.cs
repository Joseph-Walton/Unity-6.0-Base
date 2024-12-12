using System;

namespace ComponentStateMachine.Evaluate
{
    [Serializable]
    public class EvaluateTarget
    {
        public Evaluater evaluater;
        public bool target;
        public bool Evaluate()
        {
            if (evaluater.boolValue == target)
            {
                return true;
            }
            return false;
        }
    }
}

