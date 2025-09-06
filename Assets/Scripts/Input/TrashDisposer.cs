using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrashDisposer : MonoBehaviour
{
    [SerializeField] private float disposeInterval = 0.8f;

    private InputSystem_Actions inputActions;
    private Coroutine disposeCoroutine;

    private Dumpster dumpster;

    public void SetInputSystemActions(InputSystem_Actions inputActions)
    {
        this.inputActions = inputActions;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dumpster"))
        {
            dumpster = collision.GetComponent<Dumpster>();
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Dumpster"))
        {
            dumpster = null;
            StopDisposeCoroutine();
        }
    }

    private void Start()
    {
        inputActions.Player.Dispose.performed += OnDisposeStarted;
        inputActions.Player.Dispose.canceled += OnDisposeCanceled;
    }

    private void OnDisposeStarted(InputAction.CallbackContext context)
    {
        if (dumpster == null)
            return;

        if (disposeCoroutine == null)
            disposeCoroutine = StartCoroutine(DisposeRoutine());
    }

    private void OnDisposeCanceled(InputAction.CallbackContext context)
    {
        StopDisposeCoroutine();
    }

    private IEnumerator DisposeRoutine()
    {
        while (true)
        {
            Debug.Log("Dispose Trash!");
            dumpster.ConvertTrashToEnergy();

            yield return new WaitForSeconds(disposeInterval);
        }
    }

    private void StopDisposeCoroutine()
    {
        if (disposeCoroutine != null)
        {
            StopCoroutine(disposeCoroutine);
            disposeCoroutine = null;
        }
    }
}