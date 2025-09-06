using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum SoundEffectType
{
    None = 0,
    Shoot,
    PickUp,
    Load
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
            return;
        }
        Instance = this;
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
}
