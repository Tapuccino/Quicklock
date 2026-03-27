using UnityEngine;
using UnityEngine.UIElements;
using System;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    private float _currentTime;
    private bool _isPlaying;

    private TextMeshProUGUI stopwatchText;

    void Start()
    {
        _currentTime = 0;
        stopwatchText = GetComponent<TextMeshProUGUI>();

        // Start stopwatch on scene loading
        StartStopwatch();
    }
    
    void Update()
    {
        if (_isPlaying)
        {
            _currentTime += Time.deltaTime;
        }

        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        stopwatchText.text = time.ToString(@"m\:ss\:fff");
    }

    public void StartStopwatch()
    {
        _isPlaying = true;
    }

    public void StopStopwatch()
    {
        _isPlaying = false;
    }
}
