using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public void LoadGameScene()
    {
        Object.FindFirstObjectByType<AudioManager>().ChangeVolume("Theme", 0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
