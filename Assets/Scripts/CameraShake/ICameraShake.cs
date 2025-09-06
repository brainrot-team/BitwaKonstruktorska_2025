using UnityEngine;

public interface ICameraShake
{       
    Displacement CurrentDisplacement { get; }    
    bool IsFinished { get; }                    

    void Initialize(Vector3 cameraPosition, Quaternion cameraRotation);                 

    void Update(float deltaTime, Vector3 cameraPosition, Quaternion cameraRotation);    
}
