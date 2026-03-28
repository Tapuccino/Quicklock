using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void StartMusicAgain()
    {
        FindFirstObjectByType<AudioManager>().Play("Theme");
        FindFirstObjectByType<AudioManager>().Stop("WinningSong");
    }
}
