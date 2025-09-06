using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    private Queue sentences;
    public InputSystem_Actions inputActions;


    private string[] dialogue =
    {
    "YOUR MAIN GOAL IS TO PICK UP ALL THE TRASH AS FAST AS YOU CAN",
    "AMOUNT OF TRASH LEFT IS SHOWN IN UPPER LEFT CORNER",
    "PICK UP THE TRASH BY FLYING OVER IT.",
    "AMOUNT OF THRASH CARRIED IS SHOWN IN BOTTOM LEFT CORNER ABOVE THE ENERGY BAR",
    "THE MORE TRASH YOU CARRY, THE HEAVIER YOU GET AND SLOWER YOU MOVE.",
    "ON THE MAP THERE ARE ENEMIES THAT THROW TRASH AROUND.",
    "IF YOU GET SHOT BY THEM SOME OF THE THRASH YOU CARRY WILL FALL OUT OF YOUR SHIP",
    "TO DEFEAT THEM, SHOOT THEM WITH THE TRASH YOU PICKED UP USING LEFT MOUSE KEY",
    "TO AIM THRASH CANNON USE THE MOUSE",
    "YOU CAN RECYCLE PICKED UP TRASH BY GIVING IT TO \"MOTHER\" SHIP USING RIGHT MOUSE KEY",
    "RECYCLING TRASH GIVES YOU ENERGY.",
    "TO FLY YOU NEED TO USE THE ENERGY",
    "TO MOVE USE WSAD KEYS",
    "GOOD LUCK!"
};


    void Awake()
    {
        inputActions = new InputSystem_Actions();
        sentences = new Queue();

    }


    void Start()
    {
        StartTutorial(dialogue);
        inputActions.Player.Jump.performed += ctx => DisplayNextSentence();
        inputActions.Player.Skip.performed += ctx => SkipAll();
    }
    public void StartTutorial(string[] dialogue)
    {


        sentences.Clear();
        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();


    }
    public void SkipAll()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();

            return;
        }
        string sentence = (string)sentences.Dequeue();
        dialogueText.text = sentence;


    }


    void EndDialogue()
    {
        SceneManager.LoadScene("SampleScene");
    }




    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }


}
