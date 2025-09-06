using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    [Header("Aiming")]
    [SerializeField] private Transform aimSprite;

    [Header("Attacks")]
    [SerializeField] float offsetFromPlayer = 1f;
    [SerializeField] float attackSpeed = 8f;            //Bullet Speed

    [SerializeField] float delayBetweenShots = 0.5f;

    private float timeAfterShot = 0;


    private InputSystem_Actions inputActions;
    private Vector2 mouseWorldPos;

    public void SetInputSystemActions(InputSystem_Actions inputActions)
    {
        this.inputActions = inputActions;
        inputActions.Player.Attack.performed += Attack;
    }

    private void Update()
    {
        timeAfterShot+=Time.deltaTime;
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
        if (timeAfterShot < delayBetweenShots) return;
        timeAfterShot = 0;

        Vector2 direction = (mouseWorldPos - (Vector2)transform.position).normalized;
        Vector2 startVelocity = direction * attackSpeed;
        Vector2 spawnPosition = (Vector2)transform.position + direction * offsetFromPlayer;

        ProjectileSpawner.Instance.SpawnTrashProjectile(spawnPosition, startVelocity,ProjectileOrigin.Player);

        PlayerTrash.Instance.CollectedTrash--;
        SoundManager.Instance.PlaySound2(SoundEffectType.Shoot);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, offsetFromPlayer);
    }
}