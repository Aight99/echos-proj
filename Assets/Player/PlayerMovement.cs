using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private bool isRigidbodyMode;

    [SerializeField]
    private float runSpeed;

    [SerializeField]
    private float rotationSpeed;

    private CharacterController _controller;
    private Rigidbody _rigidbody;
    private PlayerInputManager _inputManager;
    private Vector3 _runDirection;

    // For CharacterController movement
    private Vector3 _currentVelocity;
    private readonly float gravityValue = -9.81f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputManager = GetComponent<PlayerInputManager>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    private void Update()
    {
        ReadInputs();

        if (!isRigidbodyMode)
        {
            HandleRun();
        }
    }

    private void FixedUpdate()
    {
        if (isRigidbodyMode)
        {
            HandleCustomRun();
        }
    }

    private void ReadInputs()
    {
        var runInput = _inputManager.InputActions.Run.ReadValue<Vector2>();
        _runDirection = new Vector3(runInput.x, 0, runInput.y);
    }

    private void HandleCustomRun()
    {
        var runDelta = runSpeed * Time.deltaTime * _runDirection;
        _rigidbody.MovePosition(_rigidbody.position + runDelta);

        if (_runDirection != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(_runDirection, Vector3.up);
            var smoothedRotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            _rigidbody.MoveRotation(smoothedRotation);
        }
    }

    private void HandleRun()
    {
        _controller.Move(runSpeed * Time.deltaTime * _runDirection);

        if (_runDirection != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(_runDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        var isGrounded = _controller.isGrounded;
        if (isGrounded && _currentVelocity.y < 0)
        {
            _currentVelocity.y = 0f;
        }

        _currentVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_currentVelocity * Time.deltaTime);
    }
}
