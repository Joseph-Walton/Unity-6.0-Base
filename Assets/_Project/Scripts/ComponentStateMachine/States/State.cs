using System.Collections.Generic;
using System;
using UnityEngine;
using ComponentStateMachine.Evaluate;

namespace ComponentStateMachine
{
    [Serializable]
    public abstract class State : MonoBehaviour, IState
    {

        [SerializeField] private State defaultChild;

        [SerializeField] private bool isComplete = false;
        public bool IsComplete 
        {
            get => isComplete;
            set
            {
                isComplete = value;
                StateCompleted?.Invoke(isComplete);
            }
        }
        [SerializeField] private bool isActive = false;
        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                ActiveChanged?.Invoke(isActive);
            }
        }

        [field: SerializeField]public State activeChildState { get; private set; }

        public List<StateTransitions> stateTransitions = new List<StateTransitions>();

        //events

        public Action<bool> ActiveChanged = delegate { };
        public Action<bool> StateCompleted = delegate { };
        private void OnEnable()
        {
            foreach (var transition in stateTransitions)
            {
                transition?.OnEnable();
                transition.EvaluatedTrue += ChangeChildState;

            }
        }
        private void OnDisable()
        {
            foreach (var transition in stateTransitions)
            {
                transition?.OnDisable();
                transition.EvaluatedTrue += ChangeChildState;
            }
        }

        public void ChangeChildState(State state)
        {
            if (activeChildState != null)
            {
                activeChildState.Exit();
                activeChildState.IsActive = false;
            }
            activeChildState = state;
            activeChildState.Enter();
            activeChildState.IsActive = true;
        }
        public void Awake()
        {
            SetUp();
        }
        public void SetUp()
        {
            if (defaultChild != null)
            {
                activeChildState = defaultChild;
                activeChildState.Enter();
                activeChildState.IsActive = true;
            }
        }
        public abstract void Enter();
        public abstract void Do();
        public abstract void FixedDo() ;
        public abstract void Exit();    

        public void BranchDo()
        {
            Do();
            if (activeChildState != null)
            {
                activeChildState.BranchDo();
            }
            
        }
        public void BranchFixedDo()
        {
            FixedDo();
            if (activeChildState != null)
            {
                activeChildState.BranchFixedDo();
            }
            
        }

    }

    public class AirState : State
    {
        public override void Do()
        {
            throw new NotImplementedException();
        }

        public override void Enter()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public override void FixedDo()
        {
            throw new NotImplementedException();
        }
    }

    public class WalkState : State
    {
        public override void Do()
        {
            throw new NotImplementedException();
        }

        public override void Enter()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public override void FixedDo()
        {
            throw new NotImplementedException();
        }
    }

    public class StateMachine : MonoBehaviour
    {

        StateNode current;
        Dictionary<Type, StateNode> nodes = new();
        HashSet<ITransition> anyTransition = new();

        ITransition GetTransition()
        {
            foreach (var transition in anyTransition)
            {
                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }

            foreach (var transition in current.Transitions)
            {
                if (transition.Condition.Evaluate())
                {
                    return transition;
                }
            }
            return null;
        }
    }

    public class StateCheck 
    {

    }
    class StateNode
    {
        public IState State { get; }
        public HashSet<Transition> Transitions { get; }

        public StateNode(IState state)
        {
            State = state;
            Transitions = new HashSet<Transition>();
        }

        public void AddTransition(IState to, IPredicate condition)
        {
            Transitions.Add(new Transition(to, condition));
        }
    }
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
    public interface IPredicate
    {
        bool Evaluate();
    }
    public class FuncPredicate : IPredicate
    {
        readonly Func<bool> func;

        public FuncPredicate(Func<bool> func)
        {
            this.func = func;
        }

        public bool Evaluate()
        {
            return func.Invoke();
        }
    }
    public interface IState
    {
        public void Enter();
        public void Do();
        public void FixedDo();
        public void Exit();
    }
    public class Transition : ITransition
    {
        public IState To { get; }
        public IPredicate Condition { get; }

        public Transition(IState to, IPredicate condition)
        {
            To = to;
            Condition = condition;
        }
    }
}

