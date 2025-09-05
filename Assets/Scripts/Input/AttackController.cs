using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour {

    [HideInInspector] public UnityEvent<int> OnNumberOfAttacksChanged = new UnityEvent<int>();
    
    [Header("Aiming")]
    [SerializeField] private Transform aimSprite; 

    [Header("Attacks")]
    [SerializeField] GameObject attackPrefab;
    [SerializeField] float attackSpeed = 8f;

    private int numberOfRemainingAttacks = 10;
    public int NumberOfRemainingAttacks {
        private set {
            numberOfRemainingAttacks--;
            if (numberOfRemainingAttacks < 0) numberOfRemainingAttacks = 0;
            OnNumberOfAttacksChanged.Invoke(numberOfRemainingAttacks);
        }
        get => numberOfRemainingAttacks;
    }

    private InputSystem_Actions inputActions;
    private Vector2 mouseWorldPos;

    public void SetInputSystemActions(InputSystem_Actions inputActions)
    {
        this.inputActions = inputActions;
        inputActions.Player.Attack.performed += Attack;
    }

    private void FixedUpdate()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = mouseWorldPos - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        aimSprite.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (numberOfRemainingAttacks <= 0) return;
        Debug.Log("Attack!");

        Vector2 direction = (mouseWorldPos - (Vector2)transform.position).normalized;
        Vector2 startVelocity = direction * attackSpeed;

        ProjectileSpawner.Instance.SpawnTrashProjectile(transform.position, startVelocity);

        NumberOfRemainingAttacks--;
    }
}