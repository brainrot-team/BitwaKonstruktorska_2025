using TMPro;
using UnityEngine;

public class EnergyUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        PlayerEnergy.OnEnergyChanged.AddListener(UpdateEnergyUI);
    }
    
    private void UpdateEnergyUI(int newEnergy)
    {
        text.text = $"Energy: {newEnergy}";
    }
}