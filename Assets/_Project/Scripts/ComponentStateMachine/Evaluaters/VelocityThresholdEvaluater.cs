using System;
using UnityEngine;

namespace ComponentStateMachine.Evaluate
{
    public class VelocityThresholdEvaluater : BaseComparisonEvaluater
    {
        [SerializeField] Rigidbody rb;

        [SerializeField] Vector3 threshold;

        [SerializeField] bool x;
        [SerializeField] bool y;
        [SerializeField] bool z;
        

        private void Update()
        {
            bool temp1, temp2, temp3;
            if (x)
            {
               temp1 = Evaluate(rb.linearVelocity.x, threshold.x);
            }
            else
            {
                temp1 = true;
            }
            if (y)
            {
                temp2 = Evaluate(rb.linearVelocity.y, threshold.y);
            }
            else
            {
                temp2 = true;
            }
            if (z)
            {
                temp3 = Evaluate(rb.linearVelocity.z, threshold.z);
            }
            else
            {
                temp3 = true;
            }

            if (temp1 && temp2 && temp3)
            {
                CheckSetValueIfChanged(true);
            }
            else
            {
                CheckSetValueIfChanged(false);
            }

        }
    }
    public abstract class BaseComparisonEvaluater : BaseEvaluater
    {
        [SerializeField] private EqualityOperations operater;

        protected bool Evaluate(float value1, float value2)
        {
            switch (operater)
            {
                case EqualityOperations.Equals:
                    return value1 == value2;
                case EqualityOperations.NotEquals:
                    return value1 != value2;
                case EqualityOperations.GreaterThan:
                    return value1 > value2;
                    case EqualityOperations .LessThan:
                    return value1 < value2;
                case EqualityOperations.LessThanOrEqual:
                    return value1 <= value2;
                case EqualityOperations.GreaterThanOrEqual:
                    return value1 >= value2;
            }
            Debug.LogError(this + " something went wrong");
            return false;
        }
        protected bool Evaluate(int value1, float value2)
        {
            switch (operater)
            {
                case EqualityOperations.Equals:
                    return value1 == value2;
                case EqualityOperations.NotEquals:
                    return value1 != value2;
                case EqualityOperations.GreaterThan:
                    return value1 > value2;
                case EqualityOperations.LessThan:
                    return value1 < value2;
                case EqualityOperations.LessThanOrEqual:
                    return value1 <= value2;
                case EqualityOperations.GreaterThanOrEqual:
                    return value1 >= value2;
            }
            Debug.LogError(this + " something went wrong");
            return false;
        }
        protected bool Evaluate(Vector3 value1, Vector3 value2)
        {
            switch (operater)
            {
                case EqualityOperations.Equals:
                    return value1 == value2;
                case EqualityOperations.NotEquals:
                    return value1 != value2;
                case EqualityOperations.GreaterThan:
                    return value1.magnitude > value2.magnitude;
                case EqualityOperations.LessThan:
                    return value1.magnitude < value2.magnitude;
                case EqualityOperations.LessThanOrEqual:
                    return value1.magnitude <= value2.magnitude;
                case EqualityOperations.GreaterThanOrEqual:
                    return value1.magnitude >= value2.magnitude;
            }
            Debug.LogError(this + " something went wrong");
            return false;
        }

    }
}

public enum EqualityOperations
{
    Equals,
    NotEquals,
    LessThan,
    GreaterThan,
    LessThanOrEqual,
    GreaterThanOrEqual,
}