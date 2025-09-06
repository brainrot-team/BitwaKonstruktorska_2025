using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Image title;
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float frameRate = 0.1f;


    [SerializeField] Button startButton;
    [SerializeField] Button tutorialButton;

    private Coroutine animationCoroutine;

    void Start()
    {
        startButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySound(SoundEffectType.Click);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        });

        tutorialButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySound(SoundEffectType.Click);
            UnityEngine.SceneManagement.SceneManager.LoadScene("ComicsScene");
        });

        Play();
    }

    public void Play()
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        for (int i = 0; i < frames.Length; i++)
        {
            title.sprite = frames[i];
            yield return new WaitForSeconds(frameRate);
        }
        animationCoroutine = null; // Animation finished, stays on last frame
    }
}
