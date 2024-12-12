using System;

namespace ComponentStateMachine.Evaluate
{
    [Serializable]
    public class EvaluateTarget
    {
        public BaseEvaluater evaluater;
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

