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
        string digits = newTrash.ToString();
        string spaced = string.Join("   ", digits.ToCharArray());
        text.text = spaced;
    }
}
