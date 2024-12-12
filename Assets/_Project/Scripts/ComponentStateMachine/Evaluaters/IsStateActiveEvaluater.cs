using UnityEngine;

namespace ComponentStateMachine.Evaluate
{
    public class IsStateActiveEvaluater : Evaluater
    {
        [SerializeField] State state;

        private void OnEnable()
        {
            state.ActiveChanged += CheckBool;
        }
        private void OnDisable()
        {
            state.ActiveChanged -= CheckBool;
        }
        private void Awake()
        {
            CheckSetValueIfChanged(state.IsActive);
        }
        public void CheckBool(bool value)
        {
            CheckSetValueIfChanged(value);
        }
    }
}

