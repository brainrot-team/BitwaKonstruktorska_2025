using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float speed = 5f;


    private AttackController attackController;

    public InputSystem_Actions inputActions;
    [HideInInspector] public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled  += OnMove;

        attackController = GetComponent<AttackController>();
        attackController.SetInputSystemActions(inputActions);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementInput = context.ReadValue<Vector2>();
        rb.linearVelocity = movementInput * speed;
    }
}
