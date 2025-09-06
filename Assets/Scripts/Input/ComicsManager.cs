using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ComicsManager : MonoBehaviour
{
    [SerializeField] int numberOfShots = 5;
    private InputSystem_Actions inputActions;
    private Animator comicsAnimator;

    void Awake()
    {
        comicsAnimator = GetComponent<Animator>();

        inputActions = new InputSystem_Actions();
        inputActions.Player.Jump.performed += OnContinue;
        inputActions.Player.Skip.performed += OnSkip;
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void OnContinue(InputAction.CallbackContext context)
    {
        int currentShotNumber = comicsAnimator.GetInteger("ShotNumber");
        comicsAnimator.SetInteger("ShotNumber", Mathf.Min(currentShotNumber + 1, numberOfShots));
        Debug.Log("Current shot number: " + (currentShotNumber + 1));
        if (currentShotNumber >= numberOfShots)
        {
            SceneManager.LoadScene("TutorialScene");
        }
    }
    
    private void OnSkip(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
