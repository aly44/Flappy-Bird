using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 3f;
    [SerializeField] private float despawnX = -12f;

    private const float PIPE_HALF_HEIGHT = 4f;

    private Transform topPipe;
    private Transform bottomPipe;

    private void Awake()
    {
        topPipe = transform.Find("TopPipe");
        bottomPipe = transform.Find("BottomPipe");
    }

    private void Update()
    {
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        if (transform.position.x < despawnX)
        {
            Destroy(gameObject);
        }
    }

    public void SetScrollSpeed(float speed)
    {
        scrollSpeed = speed;
    }

    public void SetGap(float gapSize)
    {
        float halfGap = gapSize / 2f;

        topPipe.localPosition = new Vector3(0f, halfGap + PIPE_HALF_HEIGHT, 0f);
        bottomPipe.localPosition = new Vector3(0f, -(halfGap + PIPE_HALF_HEIGHT), 0f);
    }
}
