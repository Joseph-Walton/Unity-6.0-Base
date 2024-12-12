using System;
using UnityEngine;

namespace ComponentStateMachine.Evaluate
{
    public class IsGroundedEvaluater : BaseEvaluater
    {

        [SerializeField] GroundChecker gc;
        bool IsGrounded => gc.PriorityContact.onGround;

        private void Update()
        {
            CheckSetValueIfChanged(IsGrounded);
        }
    }
}

