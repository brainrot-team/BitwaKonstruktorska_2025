using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float TimeElapsed {
        get; private set;
    }
    [SerializeField] TextMeshProUGUI minutesText, secondsText;
    private bool stopped = false;

    void Start()
    {
        TimeElapsed = 0;
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
        TimeElapsed += Time.deltaTime;
        UpdateText();
    }

    private void UpdateText()
    {
        int minutes = Mathf.FloorToInt(TimeElapsed / 60f);
        int seconds = Mathf.FloorToInt(TimeElapsed % 60f);

        minutesText.text = minutes.ToString("00");
        secondsText.text = seconds.ToString("00");

    }
}
