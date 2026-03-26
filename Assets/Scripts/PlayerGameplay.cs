using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGameplay : MonoBehaviour
{
    [Header("Assignments")]
    [SerializeField]
    private InputActionReference _spacebar;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Transform _centre;

    [SerializeField] 
    private PlayerMovement playerMovementScript;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI highScoreText;

    [SerializeField]
    private Transform _topOfLock;

    [Header("Values")]
    [SerializeField]
    private float _range;

    private int highScore = 0;

    private const int _winScore = 40;


    private void OnPress(InputAction.CallbackContext obj)
    {
        float distance = Vector2.Distance(transform.position, _target.position);

        if (distance <= _range) {
            SuccessfulHit();
        }
        else {
            FailedHit();
        }
    }


    private void SuccessfulHit()
    {
        // Send slider in opposite direction.
        playerMovementScript.clockwise = !playerMovementScript.clockwise;

        // Increase slider speed.
        playerMovementScript.speed++;

        // Move target to a random place.
        float randomDistance = Random.Range(50, 310);

        _target.RotateAround(_centre.position, Vector3.back, randomDistance);

        // Update high score
        if (highScore < playerMovementScript.speed) { highScore = playerMovementScript.speed; }

        // Update UI
        scoreText.text = playerMovementScript.speed.ToString();
        highScoreText.text = $"Highscore\n{highScore.ToString()}";

        // Loading credits scene
        if (playerMovementScript.speed == _winScore) {
            EndingSequence();
        }
    }


    private void FailedHit()
    {
        // Reset player speed
        playerMovementScript.speed = 0;

        // Update UI
        scoreText.text = playerMovementScript.speed.ToString();
    }


    private void EndingSequence()
    {
        _topOfLock.position += new Vector3(0f, 5f);

        SpriteRenderer sliderSpriteRenderer = GetComponent<SpriteRenderer>();

        Color sliderColor = sliderSpriteRenderer.color;
        sliderColor.a = 0;
        sliderSpriteRenderer.color = sliderColor;

        SpriteRenderer targetSpriteRenderer = _target.GetComponent<SpriteRenderer>();

        Color targetColor = targetSpriteRenderer.color;
        targetColor.a = 0;
        targetSpriteRenderer.color = targetColor;

        gameObject.SetActive(false);
        _target.gameObject.SetActive(false);

        Invoke("LoadCredits", 2f);
    }



    private void LoadCredits()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private void OnEnable()
    {
        _spacebar.action.started += OnPress;
    }


    private void OnDisable()
    {
        _spacebar.action.started -= OnPress;
    }
}
