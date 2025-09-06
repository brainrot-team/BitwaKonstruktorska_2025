using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameWonUI : MonoBehaviour
{
    [SerializeField] GameObject gameWonPanel;
    [SerializeField] Button restartButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] TextMeshProUGUI timeText;

    void Start()
    {
        gameWonPanel.SetActive(false);
        GameManager.OnGameWon.AddListener(ShowGameWon);

        restartButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundEffectType.Click);

            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundEffectType.Click);
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        });
    }

    private void ShowGameWon()
    {
        gameWonPanel.SetActive(true);

        float elapsed = Timer.TimeElapsed;

        int minutes = Mathf.FloorToInt(elapsed / 60f);
        int seconds = Mathf.FloorToInt(elapsed % 60f);
        timeText.text = $"TIME: {minutes:00}:{seconds:00}";
    }
}
