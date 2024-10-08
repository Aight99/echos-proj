using StateMachineUtils;
using UnityEngine;

public interface IWanderStateContext
{
    float WanderingSpeed { get; }
    float IdleTime { get; }
    Transform[] WanderingPoints { get; }
}

public class WanderState : State
{
    private readonly IWanderStateContext _context;
    private readonly NavigateState _navigateState;
    private readonly IdleState _idleState;
    private int _currentDestinationIndex = 0;

    public WanderState(
        StateMachineActor actor,
        IWanderStateContext context,
        NavigateState navigateState,
        IdleState idleState
    )
        : base(actor)
    {
        _context = context;
        _navigateState = navigateState;
        _idleState = idleState;
    }

    public override void Enter()
    {
        StartPatrol();
    }

    public override void Do()
    {
        UpdateInternalState();
    }

    private void UpdateInternalState()
    {
        if (!StateMachine.IsCompleted)
        {
            return;
        }

        if (StateMachine.CurrentState == _navigateState)
        {
            StartIdle();
        }
        else if (StateMachine.CurrentState == _idleState)
        {
            _currentDestinationIndex++;
            StartPatrol();
        }
    }

    private void StartPatrol()
    {
        StateMachine.Set(_navigateState);
        var destination = _context.WanderingPoints[
            _currentDestinationIndex % _context.WanderingPoints.Length
        ];
        _navigateState.SetDestination(destination.position, _context.WanderingSpeed);
    }

    private void StartIdle()
    {
        StateMachine.Set(_idleState);
        _idleState.SetIdle(_context.IdleTime);
    }
}
