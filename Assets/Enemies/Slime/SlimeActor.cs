using StateMachineUtils;
using UnityEngine;

public class SlimeActor : StateMachineActor
{
    [SerializeField]
    private SlimeActorContext _context;

    [SerializeField]
    private Transform _player;

    private ChaseState _chaseState;
    private WanderState _wanderState;
    private IdleState _idleState;
    private NavigateState _navigateState;

    private void Awake()
    {
        Setup();

        _navigateState = new NavigateState(this);
        _idleState = new IdleState(this);
        _wanderState = new WanderState(this, _context, _navigateState, _idleState);
        _chaseState = new ChaseState(this, _context, _navigateState);

        stateMachine.Set(_wanderState);
        _chaseState.targetTransform = _player;
    }

    private void Update()
    {
        ForceUpdateState();
        UpdateInternalState();
        stateMachine.CurrentState.UpdateTraversing();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdateTraversing();
    }

    private void UpdateInternalState()
    {
        if (!stateMachine.IsCompleted)
        {
            return;
        }

        if (stateMachine.CurrentState == _chaseState)
        {
            ColorPrint.Log("State switch!", PrintColor.Amber);
            stateMachine.Set(_idleState);
            // Not final so not in _context
            _idleState.SetIdle(5f, _player);
        }
        else if (stateMachine.CurrentState == _idleState)
        {
            stateMachine.Set(_wanderState);
        }
    }

    private void ForceUpdateState()
    {
        if (stateMachine.CurrentState != _wanderState)
        {
            return;
        }

        var isPlayerCloseEnough = IsCloseEnough(_player.position, _context.TargetingRadius);
        if (isPlayerCloseEnough)
        {
            stateMachine.Set(_chaseState);
        }
    }
}
