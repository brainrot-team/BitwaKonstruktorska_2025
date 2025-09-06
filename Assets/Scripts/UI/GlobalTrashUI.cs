using TMPro;
using UnityEngine;

public class GlobalTrashUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        WorldManager.OnTrashScoreChanged.AddListener(UpdateTrashUI);
    }

    private void UpdateTrashUI(int newTrash)
    {
        string digits = newTrash.ToString("D2"); // always 2 digits, pad with 0
        text.text = $"{digits[0]}   {digits[1]}"; // 3 spaces
    }
}
