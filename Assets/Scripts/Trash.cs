using UnityEngine;

public class Trash : MonoBehaviour
{
    private bool isPickUpDisabled = false;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isPickUpDisabled) return;
            Destroy(gameObject);
        }
    }
}
