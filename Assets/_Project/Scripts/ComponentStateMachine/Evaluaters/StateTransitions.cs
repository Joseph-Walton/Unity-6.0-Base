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

        public void OnEnable()
        {
            foreach (EvaluateTarget eval in evaluaters)
            {
                eval.evaluater.valueChanged += () => Evaluate();
            }
        }
        public void OnDisable()
        {
            foreach (EvaluateTarget eval in evaluaters)
            {
                eval.evaluater.valueChanged -= () => Evaluate();
            }
        }
        public void Evaluate()
        {
            
            foreach(EvaluateTarget eval in evaluaters)
            {
                if (!eval.Evaluate())
                {
                    Debug.Log("evaluated false");
                    return;
                }
            }
            if (stateToTransitionTo != null)
            {
                Debug.Log("evaluated true");
                EvaluatedTrue?.Invoke(stateToTransitionTo);
            }
           
        }
    }
}

