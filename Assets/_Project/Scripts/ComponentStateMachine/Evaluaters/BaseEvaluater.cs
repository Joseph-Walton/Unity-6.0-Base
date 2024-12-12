using System;
using UnityEngine;

namespace ComponentStateMachine.Evaluate
{
    [Serializable]
    public abstract class BaseEvaluater : MonoBehaviour
    {

        public Action valueChanged;
        [field: SerializeField]public bool boolValue {  get; private set; }


        public void CheckSetValueIfChanged(bool value)
        {
            if (this.boolValue == value)
            {
                return;
            }
            else
            {
                boolValue = value;
                valueChanged?.Invoke();
            }

        }

    }
}

