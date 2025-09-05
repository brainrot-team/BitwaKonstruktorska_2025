using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool isRunning = false;
    public float duration = 0.5f;

    public void Start()
    {
        if (!isRunning)
        {
            isRunning = true;
            Invoke("Stop", duration);
        }
    }

    public void Stop()
    {
        isRunning = false;
        CancelInvoke("Stop");
    }
}
