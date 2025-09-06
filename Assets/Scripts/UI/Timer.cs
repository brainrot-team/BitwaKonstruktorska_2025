using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeLeft;
    private TextMeshProUGUI text;

    void Start()
    {
        timeLeft = GameManager.Instance.GameTime;
        text = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    private void Update()
    {
        if (timeLeft > 0) {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0) {
                timeLeft = 0;
                GameManager.OnGameOver.Invoke("TIME UP!");
            }
            UpdateText();
        }
    }

    private void UpdateText()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
