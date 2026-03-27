using System.Collections;
using System.Threading;
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

    private Animator _toInvisibleSliderAnim;
    private Animator _topOfLockAnimator;


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
        // Play SFX
        Object.FindFirstObjectByType<AudioManager>().Play("HitTarget");

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

        // Win the game
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
        // Animate unlocking
        _topOfLockAnimator = _topOfLock.GetComponent<Animator>();
        _topOfLockAnimator.SetTrigger("Unlock");

        // Animate slider going invisible and getting destroyed
        _toInvisibleSliderAnim = GetComponent<Animator>();
        _toInvisibleSliderAnim.SetTrigger("StartAnimation");

        // Disable target
        _target.gameObject.SetActive(false);

        Invoke("LoadCredits", 2f);
    }

    private void DeactivateSlider()
    {
        gameObject.SetActive(false);
    }


    private void LoadCredits()
    {
        Object.FindFirstObjectByType<AudioManager>().Play("WinningSong");
        Object.FindFirstObjectByType<AudioManager>().Pause("Theme");
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
