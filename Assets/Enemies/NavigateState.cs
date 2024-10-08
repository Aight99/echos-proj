using StateMachineUtils;
using UnityEngine;

public class NavigateState : State
{
    private const float rotationSpeed = 3f;
    private Transform _targetTransform;
    private Vector3 _destination;
    private float _speed;
    private float _completeRadius;

    private Vector3 Destination =>
        _targetTransform != null ? _targetTransform.position : _destination;

    public NavigateState(StateMachineActor actor)
        : base(actor) { }

    public void SetDestination(Vector3 destination, float speed, float completeRadius = 1f)
    {
        _targetTransform = null;
        _destination = destination;
        _completeRadius = completeRadius;
        _speed = speed;
    }

    public void SetDestination(Transform target, float speed, float completeRadius = 1f)
    {
        _targetTransform = target;
        _completeRadius = completeRadius;
        _speed = speed;
    }

    public override void FixedDo()
    {
        if (actor.IsCloseEnough(Destination, _completeRadius))
        {
            IsCompleted = true;
        }
        else
        {
            Navigate();
        }
    }

    private void Navigate()
    {
        var direction = actor.HorizontalDirectionTo(Destination);
        var deltaPosition = _speed * Time.deltaTime * direction;
        actor.Rigidbody.MovePosition(actor.transform.position + deltaPosition);

        // FIXME: Дублирование в игроке и idle
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
