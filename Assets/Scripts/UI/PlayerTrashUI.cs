using UnityEngine;

public class PlayerTrashUI : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        PlayerTrash.OnNumberOfAttacksChanged.AddListener(UpdateTrashUI);
        UpdateTrashUI(PlayerTrash.Instance.CollectedTrash);
    }
    
    private void UpdateTrashUI(int newTrash)
    {
        text.text = newTrash.ToString("D2");
    }
}
