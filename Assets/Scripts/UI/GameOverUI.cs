using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] TextMeshProUGUI trashLeftText;
    [SerializeField] Button retryButton;
    [SerializeField] Button mainMenuButton;

    void Start()
    {
        gameOverPanel.SetActive(false);
        GameManager.OnGameOver.AddListener(ShowGameOver);
        retryButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundEffectType.Click);
            GameManager.Instance.ResetGame();
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundEffectType.Click);
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        });
    }

    private void ShowGameOver(string reason)
    {
        gameOverPanel.SetActive(true);
        trashLeftText.text = $"LEFT: {WorldManager.Instance.TrashScore}";
    }
}
