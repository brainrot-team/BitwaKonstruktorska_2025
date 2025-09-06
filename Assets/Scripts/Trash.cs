using UnityEngine;

public class Trash : MonoBehaviour
{
    protected bool isPickUpDisabled = false;
    
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isPickUpDisabled) return;
            Destroy(gameObject);
        }
    }
}
