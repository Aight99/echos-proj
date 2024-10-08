using StateMachineUtils;
using UnityEngine;

public class IdleState : State
{
    private float _idleTime;
    private Transform _observableTransform;
    private const float rotationSpeed = 3f;

    public IdleState(StateMachineActor actor)
        : base(actor) { }

    public void SetIdle(float idleTime, Transform observableTransform = null)
    {
        _idleTime = idleTime;
        _observableTransform = observableTransform;
    }

    public override void Do()
    {
        if (TimeInState >= _idleTime)
        {
            _observableTransform = null;
            IsCompleted = true;
        }
    }

    public override void FixedDo()
    {
        if (_observableTransform == null)
        {
            return;
        }

        var direction = actor.HorizontalDirectionTo(_observableTransform.position);
        if (direction != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            var smoothedRotation = Quaternion.Slerp(
                actor.transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            actor.Rigidbody.MoveRotation(smoothedRotation);
        }
    }
}
