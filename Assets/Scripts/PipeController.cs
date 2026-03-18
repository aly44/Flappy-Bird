using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 3f;
    [SerializeField] private float despawnX = -12f;

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
}
