using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance;
    public static CameraShakePresets Presets;

    readonly List<ICameraShake> activeShakes = new List<ICameraShake>();

    [Tooltip("Transform which will be affected by the shakes.\n\nCameraShaker will set this transform's local position and rotation.")]
    [SerializeField]
    Transform cameraTransform;
        

    [Tooltip("Scales the strength of all shakes.")]
    [Range(0, 1)]
    [SerializeField]
    public float StrengthMultiplier = 1;

    public CameraShakePresets ShakePresets;


    void Awake()
    {
        Instance = this;
        ShakePresets = new CameraShakePresets(this);
        Presets = ShakePresets;
        if (cameraTransform == null)
        {
            cameraTransform = transform;
        }
    }

    void Update()
    {
        if (cameraTransform == null) return;

        Displacement cameraDisplacement = Displacement.Zero;
        for (int i = activeShakes.Count - 1; i >= 0; i--)
        {
            if (activeShakes[i].IsFinished)
            {
                activeShakes.RemoveAt(i);
            }
            else
            {
                activeShakes[i].Update(Time.deltaTime, cameraTransform.position, cameraTransform.rotation);
                cameraDisplacement += activeShakes[i].CurrentDisplacement;
            }
        }
        cameraTransform.localPosition = StrengthMultiplier * cameraDisplacement.position;
        cameraTransform.localRotation = Quaternion.Euler(StrengthMultiplier * cameraDisplacement.eulerAngles);
    }

    // Adds a shake to the list of active shakes.
    public static void Shake(ICameraShake shake)
    {
        if (IsInstanceNull()) return;
        Instance.RegisterShake(shake);
    }

    // Adds a shake to the list of active shakes.
    public void RegisterShake(ICameraShake shake)
    {
        shake.Initialize(cameraTransform.position, cameraTransform.rotation);
        activeShakes.Add(shake);
    }

    // Sets the transform which will be affected by the shakes.
    public void SetCameraTransform(Transform cameraTransform)
    {
        cameraTransform.localPosition = Vector3.zero;
        cameraTransform.localEulerAngles = Vector3.zero;
        this.cameraTransform = cameraTransform;
    }

    //Changes value of strength
    public void ChangeShakeStrength(float newValue)
    {
        StrengthMultiplier = newValue;
    }

    private static bool IsInstanceNull()
    {
        if (Instance == null)
        {
            Debug.LogError("CameraShaker Instance is missing!");
             return true;
           }
        return false;
    }
}
