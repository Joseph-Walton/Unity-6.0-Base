using UnityEngine;
namespace ComponentStateMachine.Evaluate
{
    public class IsSprintPressedEvaluater : BaseEvaluater
    {
        [SerializeField] private InputReader inputreader;
        private void OnEnable()
        {
            inputreader.Sprint += CheckSetValueIfChanged;
        }
        private void OnDisable()
        {
            inputreader.Sprint -= CheckSetValueIfChanged;
        }
    }
}

