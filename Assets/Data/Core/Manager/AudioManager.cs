using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        { 
        Instance = this;
        DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() => PlayMusic("MainMusic");

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, s => s.Name== name);
        if (sound == null)
        {
            Debug.LogError("Песня не найдена");

        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, s => s.Name == name);
        if (sound == null)
        {
            Debug.LogError("Звук не найдена");

        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }
}
