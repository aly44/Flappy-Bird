using System.Collections.Generic;
using UnityEngine;

public class SpriteScoreDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] digitSprites;
    [SerializeField] private float digitSpacing = 0.35f;
    [SerializeField] private int sortingOrder = 10;

    private List<GameObject> digitObjects = new List<GameObject>();

    public void UpdateDisplay(int score)
    {
        // clear old digit objects before drawing new ones
        foreach (GameObject obj in digitObjects)
        {
            Destroy(obj);
        }

        digitObjects.Clear();

        string scoreStr = score.ToString();

        // center the digits around the parent position
        float startX = -(scoreStr.Length - 1) * digitSpacing / 2f;

        for (int i = 0; i < scoreStr.Length; i++)
        {
            // convert char to int, e.g '3' - '0' = 3
            int digit = scoreStr[i] - '0';

            GameObject digitObj = new GameObject("Digit_" + i);
            digitObj.transform.SetParent(transform);
            digitObj.transform.localPosition = new Vector3(startX + i * digitSpacing, 0f, 0f);
            digitObj.transform.localScale = Vector3.one;

            SpriteRenderer spriteRenderer = digitObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = digitSprites[digit];
            spriteRenderer.sortingOrder = sortingOrder;

            digitObjects.Add(digitObj);
        }
    }
}
