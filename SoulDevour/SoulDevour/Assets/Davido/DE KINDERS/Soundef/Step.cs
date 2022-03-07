using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Step : MonoBehaviour
{
    public AudioClip cry;
    public AudioClip slap;
    public AudioClip clip;
    private AudioSource audiosource;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void Cry()
    {
        audiosource.PlayOneShot(cry);
    }
    private void Slap()
    {
        audiosource.PlayOneShot(slap);
    }
    private void Walk()
    {
        audiosource.PlayOneShot(clip);
    }
}
