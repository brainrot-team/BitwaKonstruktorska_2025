using TMPro;
using UnityEngine;

public class GlobalTrashUI : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        GameManager.OnTrashInWorldChanged.AddListener(UpdateTrashUI);
    }

    private void UpdateTrashUI(int newTrash)
    {
        text.text = $"GLOBAL TRASH: {newTrash}";
    }
}
