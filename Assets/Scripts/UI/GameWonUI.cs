using UnityEngine;
using UnityEngine.UI;

public class GameWonUI : MonoBehaviour
{
    [SerializeField] Button retryButton;
    [SerializeField] GameObject gameOverPanel;

    void Start()
    {
        gameOverPanel.SetActive(false);
        GameManager.OnGameWon.AddListener(ShowGameOver);
        retryButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        });
    }
    
    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
