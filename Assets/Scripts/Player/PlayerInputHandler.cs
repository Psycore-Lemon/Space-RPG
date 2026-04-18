using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerControls Actions { get; private set; }

    public Vector2 MoveInput { get; private set; }
    public bool InteractPressedThisFrame { get; private set; }

    private void Awake()
    {
        Actions = new PlayerControls();
    }

    private void OnEnable()
    {
        Actions.Enable();

        Actions.Player.Move.performed += OnMove;
        Actions.Player.Move.canceled += OnMove;
        Actions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        Actions.Player.Move.performed -= OnMove;
        Actions.Player.Move.canceled -= OnMove;
        Actions.Player.Interact.performed -= OnInteract;

        Actions.Disable();
    }

    private void LateUpdate()
    {
        InteractPressedThisFrame = false;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<Vector2>();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        InteractPressedThisFrame = true;
    }
}
