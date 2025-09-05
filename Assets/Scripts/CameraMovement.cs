using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }

    private void Update()
    {
        transform.position = new Vector3(
            playerTransform.position.x,
            playerTransform.position.y,
            transform.position.z);
    }
}
