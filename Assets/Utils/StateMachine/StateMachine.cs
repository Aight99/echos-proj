using System.Collections.Generic;

namespace StateMachineUtils
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }
        public bool IsCompleted => CurrentState != null && CurrentState.IsCompleted;

        public void Set(State state, bool forceUpdate = false)
        {
            if (CurrentState != state || forceUpdate)
            {
                CurrentState?.Exit();
                CurrentState = state;
                CurrentState.Initialize(this);
                CurrentState.Enter();
            }
        }

#if UNITY_EDITOR
        public List<State> CurrentBranch()
        {
            if (CurrentState == null)
            {
                return new List<State>();
            }
            else
            {
                var branch = CurrentState.StateMachine.CurrentBranch();
                branch.Add(CurrentState);
                return branch;
            }
        }
#endif
    }
}
