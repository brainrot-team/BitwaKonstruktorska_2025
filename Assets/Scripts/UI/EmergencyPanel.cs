using UnityEngine;

public class EmergencyPanel : MonoBehaviour
{
    [SerializeField] private GameObject emergencyPanel;
    [SerializeField] private float flashInterval = 0.5f; // seconds between flashes

    private Coroutine flashRoutine;

    void Start()
    {
        PlayerEnergy.OnEnergyChanged.AddListener(OnEnergyChanged);
        emergencyPanel.SetActive(false); // start hidden
    }

    private void OnEnergyChanged(int newEnergy)
    {
        if (newEnergy <= 20)
        {
            if (flashRoutine == null) // start flashing if not already
                flashRoutine = StartCoroutine(FlashPanel());
        }
        else
        {
            if (flashRoutine != null) // stop flashing if energy recovers
            {
                StopCoroutine(flashRoutine);
                flashRoutine = null;
            }
            emergencyPanel.SetActive(false);
        }
    }

    private System.Collections.IEnumerator FlashPanel()
    {
        while (true)
        {
            emergencyPanel.SetActive(!emergencyPanel.activeSelf);
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
