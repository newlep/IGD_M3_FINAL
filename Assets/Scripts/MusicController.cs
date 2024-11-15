using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip backgroundMusic; // Drag the music file here in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Add and configure the Audio Source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = true;

        // Play the music
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
