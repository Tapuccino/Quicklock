using System;
using UnityEngine.SceneManagement;
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

    /// <summary>
    /// Input 2 pitches to randomise between, or none to not randomise pitch.
    /// </summary>
    /// <param name="randomLowerPitch">Inclusive</param>
    /// <param name="randomHigherPitch">Inclusive</param>
    public void Play(string name, float randomLowerPitch, float randomHigherPitch)
    {
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == name);

        if (soundToPlay == null)
        {
            Debug.Log("Sound not found to play"); return;
        }

        soundToPlay.source.pitch = UnityEngine.Random.Range(randomLowerPitch, randomHigherPitch);
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

    public void Stop(string name)
    {
        Sound soundToStop = Array.Find(sounds, sound => sound.name == name);

        if (soundToStop == null)
        {
            Debug.Log("Sound not found to pause");
        }

        soundToStop.source.Stop();
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

    public float GetCurrentVolume(string name)
    {
        Sound soundToGetVolume = Array.Find(sounds, sound => sound.name == name);

        if (soundToGetVolume == null)
        {
            Debug.Log("Sound not found to get volume");
            return 0;
        }

        float returnVolume = soundToGetVolume.source.volume;
        return returnVolume;
    }
}
