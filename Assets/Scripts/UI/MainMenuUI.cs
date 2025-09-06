using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        });    
    }

}
