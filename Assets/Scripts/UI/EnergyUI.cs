using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] Image sliderImage;

    private void Start()
    {
        PlayerEnergy.OnEnergyChanged.AddListener(UpdateEnergyUI);
    }
    
    private void UpdateEnergyUI(int newEnergy)
    {
        sliderImage.fillAmount = newEnergy / (float)PlayerEnergy.MaxEnergy;
    }
}