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
        text.text = newTrash.ToString("D4");
    }
}
