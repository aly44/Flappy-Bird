using System.Collections.Generic;
using UnityEngine;

public class BaseScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 3f;

    private List<Transform> tiles = new List<Transform>();
    private float tileWidth;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            tiles.Add(child);
        }
    }

    private void Start()
    {
        tileWidth = tiles[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        // ground stops moving when dead, matches original flappy bird
        if (GameManager.Instance != null && GameManager.Instance.State == GameState.Dead)
        {
            return;
        }

        float cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        foreach (Transform tile in tiles)
        {
            tile.Translate(Vector2.left * scrollSpeed * Time.deltaTime, Space.World);
        }

        // when a tile goes off screen left, snap it behind the rightmost tile
        foreach (Transform tile in tiles)
        {
            if (tile.position.x + tileWidth / 2f < -cameraHalfWidth)
            {
                float maxX = float.NegativeInfinity;

                foreach (Transform t in tiles)
                {
                    maxX = Mathf.Max(maxX, t.position.x);
                }

                tile.position = new Vector3(maxX + tileWidth, tile.position.y, tile.position.z);
            }
        }
    }
}
