using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip flapClip;
    [SerializeField] private AudioClip scoreClip;
    [SerializeField] private AudioClip deathClip;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayFlap()
    {
        audioSource.PlayOneShot(flapClip);
    }

    public void PlayScore()
    {
        audioSource.PlayOneShot(scoreClip);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(deathClip);
    }
}
