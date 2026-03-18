using UnityEngine;

public class BirdAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float frameRate = 8f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.State == GameState.Dead)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= 1f / frameRate)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;
            spriteRenderer.sprite = frames[currentFrame];
        }
    }
}
