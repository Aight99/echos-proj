using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerInputsActions _input;
    public PlayerInputsActions.PlayerActions InputActions { get; private set; }

    private void Awake()
    {
        _input = new PlayerInputsActions();
        InputActions = _input.Player;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
