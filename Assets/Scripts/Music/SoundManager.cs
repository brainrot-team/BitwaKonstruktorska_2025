using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource sfxSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public void PlaySound(AudioClip AudioToPlay)
    {
        sfxSound.clip = AudioToPlay;
        sfxSound.Play();
    }
}
