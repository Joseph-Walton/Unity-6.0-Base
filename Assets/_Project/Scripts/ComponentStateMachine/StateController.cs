using UnityEngine;

namespace ComponentStateMachine
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private State rootState;

        private void Start()
        {
            rootState.Enter();
            rootState.IsActive = true;
        }
        private void Update()
        {
            rootState.BranchDo();
        }
        private void FixedUpdate()
        {
            rootState.BranchFixedDo();
        }
    }
}

