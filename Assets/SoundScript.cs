using UnityEngine;

public class SoundScript : MonoBehaviour
{
 
    public static SoundScript Instance;

    private void Awake()
    {
        Instance = this;
    }



    public void PlaySound( AudioClip AudioToPlay )
    {
        GetComponent<AudioSource>().clip = AudioToPlay;
        GetComponent<AudioSource>().Play();
    }

}
