using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();

        SetVolumeSlider(audioManager.GetCurrentVolume("Theme"));
    }

    public void ChangeMusicVolume(float value)
    {
        audioManager.ChangeVolume("Theme", value);
        audioManager.ChangeVolume("WinningSong", value);
    }

    private void SetVolumeSlider(float value)
    {
        Slider slider = GetComponent<Slider>();

        slider.value = value;
    }
}
