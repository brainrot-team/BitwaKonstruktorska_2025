using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeElapsed;
    [SerializeField] TextMeshProUGUI minutesText, secondsText;
    private bool stopped = false;

    void Start()
    {
        timeElapsed = 0;
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
        timeElapsed += Time.deltaTime;
        UpdateText();
    }

    private void UpdateText()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(timeElapsed % 60f);

        minutesText.text = minutes.ToString("00");
        secondsText.text = seconds.ToString("00");

    }
}
