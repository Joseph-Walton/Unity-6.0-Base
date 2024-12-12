using System.Collections.Generic;
using System;
using UnityEngine;

namespace ComponentStateMachine.Evaluate
{
    [Serializable]
    public class StateTransitions
    {

        public Action<State> EvaluatedTrue = delegate(State state) { };
        public State stateToTransitionTo;
        [SerializeField] private List<EvaluateTarget> evaluaters = new List<EvaluateTarget>();
        public StateTransitions()
        {
            foreach (EvaluateTarget eval in evaluaters)
            {
                eval.evaluater.valueChanged += () => Evaluate();
            }
        }
        ~StateTransitions()
        {
            foreach (EvaluateTarget eval in evaluaters)
            {
                eval.evaluater.valueChanged -= () => Evaluate();
            }
        }
        public void Evaluate()
        {
            Debug.Log("evaluate");
            foreach(EvaluateTarget eval in evaluaters)
            {
                if (!eval.Evaluate())
                {
                    return;
                }
            }
            if (stateToTransitionTo != null)
            {
                EvaluatedTrue?.Invoke(stateToTransitionTo);
            }
           
        }
    }
}

