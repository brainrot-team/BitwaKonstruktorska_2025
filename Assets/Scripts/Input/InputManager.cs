using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AttackController))]
public class InputManager : MonoBehaviour
{
    [Header("Other")]
    [SerializeField] private TrashDisposer trashDisposer;


    [Header("Movement Settings")]
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float speedDecreasePerAttack = 0.4f;
    [SerializeField] int startEnergy = 10;
    [SerializeField] float energyDecreaseInterval = 2f;

    private IEnumerator energyDepletionCoroutine;
    private float currentSpeed;

    private AttackController attackController;

    public InputSystem_Actions inputActions;
    [HideInInspector] public Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputActions = new InputSystem_Actions();

        attackController = GetComponent<AttackController>();
        attackController.SetInputSystemActions(inputActions);

        trashDisposer.SetInputSystemActions(inputActions);

        PlayerTrash.OnNumberOfAttacksChanged.AddListener(ChangePlayerSpeed);

        currentSpeed = maxSpeed;
        PlayerEnergy.Energy = startEnergy;
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
        if (movementInput.magnitude == 0)
        {
            StopEnergyDepletionCoroutine();
            animator.SetBool("isDriving", false);
        }
        else
        {
            if (energyDepletionCoroutine == null)
            {
                energyDepletionCoroutine = DecreaseEnergy();
                StartCoroutine(energyDepletionCoroutine);
            }
        }
        rb.AddForce(movementInput * currentSpeed);
    }

    private void ChangePlayerSpeed(int numberOfRemainingAttacks)
    {
        currentSpeed = Mathf.Clamp(maxSpeed - numberOfRemainingAttacks * speedDecreasePerAttack, 1, maxSpeed);
    }

    private IEnumerator DecreaseEnergy()
    {
        animator.SetBool("isDriving", true);
        while (true)
        {
            //Debug.Log("Energy Decreased!: " + PlayerEnergy.Energy);
            PlayerEnergy.Energy--;
            if (PlayerEnergy.Energy <= 0)
            {
                GameManager.OnGameOver.Invoke("OUT OF ENERGY!");
                SoundManager.Instance?.PlaySound(SoundEffectType.Lose);
            }
            yield return new WaitForSeconds(energyDecreaseInterval);
        }
    }

    
    private void StopEnergyDepletionCoroutine()
    {
        if (energyDepletionCoroutine != null)
        {
            StopCoroutine(energyDepletionCoroutine);
            energyDepletionCoroutine = null;
        }
    }

    public void HitByProjectile()
    {
        print("hit by projectile");
        PerformShake.instance.TriggerShake(transform.position);
        PlayerEnergy.Energy -= 5;
    }
}
