using UnityEngine;

namespace StateMachineUtils
{
    public abstract class State
    {
        public bool IsCompleted { get; protected set; }
        public StateMachine StateMachine { get; protected set; }
        private float startTime;
        protected float TimeInState => Time.time - startTime;
        protected StateMachineActor actor;

        protected State(StateMachineActor actor)
        {
            this.actor = actor;
            StateMachine = new StateMachine();
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Do() { }

        public virtual void FixedDo() { }

        public void UpdateTraversing()
        {
            Do();
            StateMachine?.CurrentState?.UpdateTraversing();
        }

        public void FixedUpdateTraversing()
        {
            FixedDo();
            StateMachine?.CurrentState?.FixedUpdateTraversing();
        }

        public void Initialize(StateMachine rootStateMachine)
        {
            IsCompleted = false;
            startTime = Time.time;
        }
    }
}
