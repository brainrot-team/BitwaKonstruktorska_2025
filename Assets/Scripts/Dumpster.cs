using UnityEngine;

public class Dumpster : MonoBehaviour 
{
    public void ConvertTrashToEnergy()
    {
        PlayerTrash.Instance.CollectedTrash--;
        PlayerEnergy.Energy++;
    }
}