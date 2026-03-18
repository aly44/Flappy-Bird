using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float flapForce = 7f;
    [SerializeField] private float topBound = 4.5f;
    [SerializeField] private float bottomBound = -4.5f;
    [SerializeField] private float rotationSpeed = 300f;

    private Rigidbody2D rigidBody;
    private float initialGravityScale;
    private Vector3 initialScale;
    private Vector3 initialPosition;
    private bool isDead = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        initialGravityScale = rigidBody.gravityScale;
        initialScale = transform.localScale;
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        // wait for first tap before starting
        if (GameManager.Instance != null && GameManager.Instance.State == GameState.Idle)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.StartGame();
                Flap();
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Flap();
        }

        // tilt up when going up, nose dive when falling, multiplying by 4 felt right
        float targetAngle = Mathf.Clamp(rigidBody.linearVelocity.y * 4f, -90f, 30f);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(0f, 0f, targetAngle),
            rotationSpeed * Time.deltaTime
        );

        if (transform.position.y > topBound || transform.position.y < bottomBound)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance != null && GameManager.Instance.State == GameState.Idle)
        {
            return;
        }

        Die();
    }

    private void Flap()
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, flapForce);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayFlap();
        }

        StopCoroutine(nameof(SquashStretchRoutine));
        StartCoroutine(nameof(SquashStretchRoutine));
    }

    private IEnumerator SquashStretchRoutine()
    {
        // squash on flap then lerp back to normal
        Vector3 stretchedScale = new Vector3(initialScale.x * 0.7f, initialScale.y * 1.4f, 1f);
        float duration = 0.2f;
        float elapsed = 0f;

        transform.localScale = stretchedScale;

        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(stretchedScale, initialScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = initialScale;
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.gravityScale = 0f;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayDeath();
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnBirdDied();
        }
    }

    public void StartPlaying()
    {
        rigidBody.gravityScale = initialGravityScale;
    }

    public void ResetBird()
    {
        StopAllCoroutines();
        isDead = false;
        // gravity off until player taps
        rigidBody.gravityScale = 0f;
        rigidBody.linearVelocity = Vector2.zero;
        transform.position = initialPosition;
        transform.localScale = initialScale;
        transform.rotation = Quaternion.identity;
    }
}
