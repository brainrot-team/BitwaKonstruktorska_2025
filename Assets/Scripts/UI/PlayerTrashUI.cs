using UnityEngine;

public class PlayerTrashUI : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        PlayerTrash.OnNumberOfAttacksChanged.AddListener(UpdateTrashUI);
    }
    
    private void UpdateTrashUI(int newTrash)
    {
        string digits = newTrash.ToString();
        string spaced = string.Join("   ", digits.ToCharArray());
        text.text = spaced;
    }
}
