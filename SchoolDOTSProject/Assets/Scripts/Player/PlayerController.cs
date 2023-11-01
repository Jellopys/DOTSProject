using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerWeapon playerWeapon;
    private PlayerInputActions playerInputActions;
    private PlayerInput playerInput;

    private Vector2 movement;
    private Vector2 aim;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerWeapon = GetComponent<PlayerWeapon>();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();

    }

    void Update()
    {
        HandleInput();
        playerMovement.HandleMovement(movement);
        playerMovement.HandleRotation(aim);
        
    }

    void HandleInput()
    {
        movement = playerInputActions.Controls.Movement.ReadValue<Vector2>();
        aim = playerInputActions.Controls.Aim.ReadValue<Vector2>();

        if (playerInputActions.Controls.Fire.IsPressed())
        {
            playerWeapon.FireWeapon();
        }
    }
}
