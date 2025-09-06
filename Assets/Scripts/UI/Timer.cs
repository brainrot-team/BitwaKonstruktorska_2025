using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeElapsed;
    private TextMeshProUGUI text;
    private bool stopped = false;

    void Start()
    {
        timeElapsed = 0;
        text = GetComponent<TextMeshProUGUI>();
        UpdateText();
        GameManager.OnGameOver.AddListener((_) =>
        {
            stopped = true;
        });
        GameManager.OnGameWon.AddListener(() =>
        {
            stopped = true;
        });
    }

    private void Update()
    {
        if(stopped) return;
        if (timeElapsed > 0)
        {
            timeElapsed += Time.deltaTime;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(timeElapsed % 60f);
        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
