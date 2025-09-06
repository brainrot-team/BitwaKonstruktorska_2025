using UnityEngine;

public class PerformShake : MonoBehaviour
{
    [SerializeField] private CameraShaker shaker;

    [SerializeField] private float positionStrength = 1f; 
    [SerializeField] private float rotationStrength = 3;
    [SerializeField] private float duration = 0.5f;


    public static PerformShake instance;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerShake(Vector2 desiredDirection)
    {
        CameraShaker.Presets.Explosion2D(positionStrength,rotationStrength,duration);
    }

    void Update()
    {
        //TriggerShake(transform.position);
    }
}
