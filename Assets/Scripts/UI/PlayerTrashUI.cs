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
        string digits = newTrash.ToString("D2"); // always 2 digits, pad with 0
        text.text = $"{digits[0]}   {digits[1]}"; // 3 spaces
    }
}
