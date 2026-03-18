using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipePairPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnX = 12f;
    [SerializeField] private float gapCenterMin = -2f;
    [SerializeField] private float gapCenterMax = 2f;

    [SerializeField] private float gapSizeStart = 3f;
    [SerializeField] private float gapSizeMin = 1.8f;
    [SerializeField] private float gapShrinkAmount = 0.15f;

    [SerializeField] private float scrollSpeedStart = 3f;
    [SerializeField] private float scrollSpeedMax = 6f;
    [SerializeField] private float scrollSpeedIncrease = 0.2f;

    [SerializeField] private int pipesPerDifficultyStep = 5;

    private float currentGapSize;
    private float currentScrollSpeed;
    private int pipesSpawned;
    private float spawnTimer;
    private bool isActive;

    private void Start()
    {
        currentGapSize = gapSizeStart;
        currentScrollSpeed = scrollSpeedStart;
        pipesSpawned = 0;
        spawnTimer = spawnInterval;
        isActive = true;
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnPipe();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnPipe()
    {
        float gapCenterY = Random.Range(gapCenterMin, gapCenterMax);
        Vector3 spawnPosition = new Vector3(spawnX, gapCenterY, 0f);

        GameObject pipePair = Instantiate(pipePairPrefab, spawnPosition, Quaternion.identity);
        PipeController pipeController = pipePair.GetComponent<PipeController>();

        pipeController.SetScrollSpeed(currentScrollSpeed);
        pipeController.SetGap(currentGapSize);
        SetRandomPipeColor(pipePair);

        pipesSpawned++;

        if (pipesSpawned % pipesPerDifficultyStep == 0)
        {
            IncreaseDifficulty();
        }
    }

    private void SetRandomPipeColor(GameObject pipePair)
    {
        float green = Random.Range(0.55f, 0.9f);
        Color pipeColor = new Color(0.15f, green, 0.25f, 1f);

        SpriteRenderer topRenderer = pipePair.transform.Find("TopPipe").GetComponent<SpriteRenderer>();
        SpriteRenderer bottomRenderer = pipePair.transform.Find("BottomPipe").GetComponent<SpriteRenderer>();

        topRenderer.color = pipeColor;
        bottomRenderer.color = pipeColor;
    }

    private void IncreaseDifficulty()
    {
        currentGapSize = Mathf.Max(currentGapSize - gapShrinkAmount, gapSizeMin);
        currentScrollSpeed = Mathf.Min(currentScrollSpeed + scrollSpeedIncrease, scrollSpeedMax);
    }

    public void StartSpawning()
    {
        currentGapSize = gapSizeStart;
        currentScrollSpeed = scrollSpeedStart;
        pipesSpawned = 0;
        spawnTimer = spawnInterval;
        isActive = true;
    }

    public void StopSpawning()
    {
        isActive = false;
    }
}
