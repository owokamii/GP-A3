using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip[] music;
    public AudioClip[] sfx;

    private void Start()
    {
            musicSource.clip = music[0];
            musicSource.Play();
    }

    private void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            musicSource.UnPause();
        }

        else
        {
            musicSource.Pause();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
