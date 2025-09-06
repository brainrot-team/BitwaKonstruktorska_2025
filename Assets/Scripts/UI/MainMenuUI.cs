using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button tutorialButton;

    void Start()
    {
        startButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySound(SoundEffectType.Click);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        });

        tutorialButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySound(SoundEffectType.Click);
            UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialScene");
        });
    }


}
