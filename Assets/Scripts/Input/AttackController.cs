using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [Header("Aiming")]
    [SerializeField] private Transform aimSprite;

    [Header("Attacks")]
    [SerializeField] float offsetFromPlayer = 1f;
    [SerializeField] float attackSpeed = 8f;


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
        if (PlayerTrash.Instance.CollectedTrash <= 0) return;
        Debug.Log("Attack!");

        Vector2 direction = (mouseWorldPos - (Vector2)transform.position).normalized;
        Vector2 startVelocity = direction * attackSpeed;
        Vector2 spawnPosition = (Vector2)transform.position + direction * offsetFromPlayer;

        ProjectileSpawner.Instance.SpawnTrashProjectile(spawnPosition, startVelocity);

        PlayerTrash.Instance.CollectedTrash--;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, offsetFromPlayer);
    }
}