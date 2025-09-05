using AYellowpaper.SerializedCollections;
using UnityEngine;

public class ParallaxController : MonoBehaviour 
{
    private Vector3 lastCameraPosition;

    private Camera mainCamera;

    // Backgrounds with it's parallax speed
    [SerializeField] SerializedDictionary<Transform, float> backgrounds = new SerializedDictionary<Transform, float>();

    void Start()
    {
        mainCamera = Camera.main;
        lastCameraPosition = mainCamera.transform.position;
    }

    void LateUpdate()
    {
        Vector3 delta = mainCamera.transform.position - lastCameraPosition;

        foreach (var kvp in backgrounds)
        {
            Transform background = kvp.Key;
            float speed = kvp.Value;

            // Move background in opposite direction of camera movement
            Vector3 newPos = background.position + new Vector3(delta.x * speed, delta.y * speed, 0);
            background.position = newPos;
        }

        lastCameraPosition = mainCamera.transform.position;
    }

#if UNITY_EDITOR
    private void OnValidate() 
    {
        if(backgrounds.Count == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                backgrounds.Add(transform.GetChild(i), 0.1f * (transform.childCount - i) + 0.1f);
            }
        }    
    }
#endif
}