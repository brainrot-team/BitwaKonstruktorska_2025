using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum SoundEffectType
{
    None = 0,
    Shoot,
    PickUp,
    Load,
    EnemyShoot,
    Click,
    Win,
    Lose,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] float volumeRange = 0.2f;
    [SerializeField] float pitchRange = 0.2f;

    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] SerializedDictionary<SoundEffectType, AudioSource> levelSoundEffects;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    
    public void PlaySound(SoundEffectType soundEffectType)
    {
        if (!levelSoundEffects.ContainsKey(soundEffectType)) return;

        AudioSource source = levelSoundEffects[soundEffectType];
        if (source.isPlaying) return;
        source.volume = Random.Range(1f - volumeRange, 1f + volumeRange);
        source.pitch = Random.Range(1f - pitchRange, 1f + pitchRange);
        source.Play();
    }
    public void PlaySound2(SoundEffectType soundEffectType)
    {
        if (!levelSoundEffects.ContainsKey(soundEffectType)) return;

        AudioSource source = levelSoundEffects[soundEffectType];
        if (source.isPlaying) return;
        source.volume = Random.Range(1f - volumeRange, 1f + volumeRange);
        source.pitch = Random.Range(0.2f, 0.3f);
        source.Play();
    }

    public void PlaySound3(SoundEffectType soundEffectType)
    {
        if (!levelSoundEffects.ContainsKey(soundEffectType)) return;

        AudioSource source = levelSoundEffects[soundEffectType];
        if (source.isPlaying) return;
        source.volume = Random.Range(1.5f - volumeRange, 1.5f + volumeRange);
        source.pitch = Random.Range(0.2f, 0.3f);
        source.Play();
    }
}
