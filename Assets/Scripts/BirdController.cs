using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float flapForce = 7f;
    [SerializeField] private float topBound = 4.5f;
    [SerializeField] private float bottomBound = -4.5f;

    private Rigidbody2D rigidBody;
    private float initialGravityScale;
    private bool isDead = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        initialGravityScale = rigidBody.gravityScale;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Flap();
        }

        if (transform.position.y > topBound || transform.position.y < bottomBound)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void Flap()
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, flapForce);
        StopCoroutine(nameof(SquashStretchRoutine));
        StartCoroutine(nameof(SquashStretchRoutine));
    }

    private IEnumerator SquashStretchRoutine()
    {
        Vector3 stretchedScale = new Vector3(0.7f, 1.4f, 1f);
        float duration = 0.2f;
        float elapsed = 0f;

        transform.localScale = stretchedScale;

        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(stretchedScale, Vector3.one, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one;
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

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnBirdDied();
        }
    }

    public void ResetBird()
    {
        StopAllCoroutines();
        isDead = false;
        rigidBody.gravityScale = initialGravityScale;
        rigidBody.linearVelocity = Vector2.zero;
        transform.position = new Vector3(-3f, 0f, 0f);
        transform.localScale = Vector3.one;
    }
}
