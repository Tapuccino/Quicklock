using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [Header("Values")]
    [SerializeField]
    private float _range;

    private int highScore = 0;


    private void OnPress(InputAction.CallbackContext obj)
    {
        float distance = Vector2.Distance(transform.position, _target.position);

        if (distance <= _range)
        {
            SuccessfulHit();
        }
        else
        {
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
    }


    private void FailedHit()
    {
        // Reset player speed
        playerMovementScript.speed = 0;

        // Update UI
        scoreText.text = playerMovementScript.speed.ToString();
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
