using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
namespace ComponentStateMachine.Evaluate
{
    public class IsJumpedPressedEvaluater : BaseEvaluater
    {
        [SerializeField] private InputReader inputreader;
        private void OnEnable()
        {
            inputreader.Jump += CheckSetValueIfChanged;
        }
        private void OnDisable()
        {
            inputreader.Jump -= CheckSetValueIfChanged;
        }
    }
}

