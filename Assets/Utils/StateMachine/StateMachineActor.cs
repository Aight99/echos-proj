using UnityEngine;

namespace StateMachineUtils
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class StateMachineActor : MonoBehaviour
    {
        [SerializeField]
        private DebugConfig _debugConfig;
        public Rigidbody Rigidbody { get; private set; }
        protected StateMachine stateMachine;

        protected void Setup()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.isKinematic = true;
            Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            stateMachine = new StateMachine();
        }

        public bool IsCloseEnough(
            Vector3 targetPosition,
            float targetingRadius,
            float threshold = 1f
        )
        {
            var distance = Vector3.Distance(transform.position, targetPosition);
            return distance <= targetingRadius + threshold;
        }

        public Vector3 HorizontalDirectionTo(Vector3 target)
        {
            return new Vector3(
                target.x - transform.position.x,
                0,
                target.z - transform.position.z
            ).normalized;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _debugConfig.color;
            Vector3 startPosition = transform.position;
            Vector3 direction = transform.forward;
            Gizmos.DrawRay(startPosition, direction * _debugConfig.rayLength);

            if (Application.isPlaying && stateMachine.CurrentState != null)
            {
                var stateBranch = stateMachine.CurrentBranch();
                UnityEditor.Handles.Label(transform.position, string.Join("\n", stateBranch));
            }

            Gizmos.DrawWireSphere(transform.position, _debugConfig.areaRadius);
        }
#endif
    }

    [System.Serializable]
    public struct DebugConfig
    {
        [SerializeField]
        public Color color;

        [SerializeField]
        public float rayLength;

        [SerializeField]
        public float areaRadius;
    }
}
