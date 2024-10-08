using UnityEngine;

[System.Serializable]
public struct SlimeActorContext : IChaseStateContext, IWanderStateContext
{
    [SerializeField]
    private float _wanderingSpeed;

    [SerializeField]
    private float _chaiseSpeed;

    [SerializeField]
    private float _idleTime;

    [SerializeField]
    private float _targetingRadius;

    [SerializeField]
    private Transform[] _wanderingPoints;

    public readonly float ChaiseSpeed => _chaiseSpeed;

    public readonly float TargetingRadius => _targetingRadius;

    public readonly float WanderingSpeed => _wanderingSpeed;

    public readonly float IdleTime => _idleTime;

    public readonly Transform[] WanderingPoints => _wanderingPoints;
}
