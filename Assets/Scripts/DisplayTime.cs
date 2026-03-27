using TMPro;
using UnityEngine;

public class DisplayTime : MonoBehaviour
{
    private TextMeshProUGUI tMP;

    void Start()
    {
        // Set tmp element
        tMP = GetComponent<TextMeshProUGUI>();

        // Set text to static stopwatch time
        tMP.text = Stopwatch.FinalTime;
    }
}
