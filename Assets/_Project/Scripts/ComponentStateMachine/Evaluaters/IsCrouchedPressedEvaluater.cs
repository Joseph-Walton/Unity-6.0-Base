using UnityEngine;
namespace ComponentStateMachine.Evaluate
{
    public class IsCrouchedPressedEvaluater : BaseEvaluater
    {
        [SerializeField] private InputReader inputreader;
        private void OnEnable()
        {
            inputreader.Crouching += CheckSetValueIfChanged;
        }
        private void OnDisable()
        {
            inputreader.Crouching -= CheckSetValueIfChanged;
        }
    }
}

