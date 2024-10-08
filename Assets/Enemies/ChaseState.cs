using StateMachineUtils;
using UnityEngine;

public interface IChaseStateContext
{
    float ChaiseSpeed { get; }
    float TargetingRadius { get; }
}

public class ChaseState : State
{
    private readonly IChaseStateContext _context;
    private readonly NavigateState _navigateState;
    public Transform targetTransform;

    public ChaseState(
        StateMachineActor actor,
        IChaseStateContext context,
        NavigateState navigateState
    )
        : base(actor)
    {
        _context = context;
        _navigateState = navigateState;
    }

    public override void Enter()
    {
        StateMachine.Set(_navigateState);
        _navigateState.SetDestination(targetTransform, _context.ChaiseSpeed, .1f);
    }

    public override void Do()
    {
        var isTargetCloseEnough = actor.IsCloseEnough(
            targetTransform.position,
            _context.TargetingRadius
        );
        if (StateMachine.IsCompleted || !isTargetCloseEnough)
        {
            IsCompleted = true;
        }
    }
}
