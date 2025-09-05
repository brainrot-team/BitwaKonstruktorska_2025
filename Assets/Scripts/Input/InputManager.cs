using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float speedDecreasePerAttack = 0.4f;
    private float currentSpeed;

    private AttackController attackController;

    public InputSystem_Actions inputActions;
    [HideInInspector] public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();

        attackController = GetComponent<AttackController>();
        attackController.SetInputSystemActions(inputActions);
        attackController.OnNumberOfAttacksChanged.AddListener(ChangePlayerSpeed);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 movementInput = inputActions.Player.Move.ReadValue<Vector2>();
		rb.AddForce(movementInput * currentSpeed);
        
    }

    private void ChangePlayerSpeed(int numberOfRemainingAttacks)
    {
        Debug.Log("gowno");
        currentSpeed = Mathf.Clamp(maxSpeed - numberOfRemainingAttacks * speedDecreasePerAttack, 1, maxSpeed);
    }
}
