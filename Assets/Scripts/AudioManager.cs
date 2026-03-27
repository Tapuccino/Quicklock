using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;

            sound.source.loop = sound.looping;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == name);

        if (soundToPlay == null) 
        { 
            Debug.Log("Sound not found to play"); return; 
        }

        soundToPlay.source.Play();
    }

    public void Pause(string name)
    {
        Sound soundToPause = Array.Find(sounds, sound => sound.name == name);

        if (soundToPause == null)
        {
            Debug.Log("Sound not found to pause");
        }

        soundToPause.source.Pause();
    }

    /// <summary>
    /// volume must be between 0-1
    /// </summary>
    public void ChangeVolume(string name, float volume)
    {
        Sound soundToChangeVolume = Array.Find(sounds, sound => sound.name == name);

        if (soundToChangeVolume == null)
        {
            Debug.Log("Sound not found to change volume");
            return;
        }

        soundToChangeVolume.source.volume = Mathf.Clamp01(volume);
    }
}
