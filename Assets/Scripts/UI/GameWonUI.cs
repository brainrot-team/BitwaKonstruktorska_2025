using UnityEngine;
using UnityEngine.UI;

public class GameWonUI : MonoBehaviour
{
    [SerializeField] GameObject gameWonPanel;

    void Start()
    {
        gameWonPanel.SetActive(false);
        GameManager.OnGameWon.AddListener(ShowGameWon);
    }
    
    private void ShowGameWon()
    {
        gameWonPanel.SetActive(true);
    }
}
