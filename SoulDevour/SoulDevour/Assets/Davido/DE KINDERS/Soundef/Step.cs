using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Step : MonoBehaviour
{
    public AudioClip cry;
    public AudioClip slap;
    public AudioClip clip;
    private AudioSource audiosource;
    public ParticleSystem walking;
    public ParticleSystem kicking;
   
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
        walking.Play();
    }

    private void Kick()
    {
        audiosource.PlayOneShot(slap);
        kicking.Play();
    }
}
