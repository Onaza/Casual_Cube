using UnityEngine;

public class MatchEffecft : MonoBehaviour
{
    private ParticleSystem particle;
    private AudioSource audioSrc;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        particle.Play();
        audioSrc.Play();
    }

    void Update()
    {
        if(audioSrc.isPlaying == false)
            gameObject.SetActive(false);
    }
}
