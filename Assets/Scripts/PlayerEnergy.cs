using UnityEngine;
using UnityEngine.Events;

public class PlayerEnergy
{
    [HideInInspector] public static UnityEvent<int> OnEnergyChanged = new UnityEvent<int>();

    public static int MaxEnergy { get; } = 100;
    private static int energy = 100;
    public static int Energy
    {
        get => energy;
        set
        {
            energy = value;
            OnEnergyChanged.Invoke(value);
        }
    }
    
}