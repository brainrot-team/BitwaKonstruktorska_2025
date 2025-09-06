using UnityEngine;

public class Dumpster : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();        
    }

    public void ConvertTrashToEnergy()
    {
        PlayerTrash.Instance.CollectedTrash--;
        PlayerEnergy.Energy++;
    }

    public void OnPlayerEnter()
    {
        animator.SetTrigger("open");
    }

    public void OnPlayerExit()
    {
        animator.SetTrigger("close");
    }
}