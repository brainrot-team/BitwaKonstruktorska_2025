using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] Image sliderImage;

    private void Start()
    {
        PlayerEnergy.OnEnergyChanged.AddListener(UpdateEnergyUI);
        UpdateEnergyUI(PlayerEnergy.Energy);
    }
    
    private void UpdateEnergyUI(int newEnergy)
    {
        sliderImage.fillAmount = newEnergy / (float)PlayerEnergy.MaxEnergy;
    }
}