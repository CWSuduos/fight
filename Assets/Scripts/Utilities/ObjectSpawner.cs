using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab;                  // ������, ������� ����� ��������������
    public float spawnRadius = 100f;           // ������ ������� ��������� ��������
    public float spawnInterval = 4f;           // �������� ��������� �������� � ��������
    public int initialNumberOfObjects = 25;    // ��������� ���������� �������� ��� ���������
    public int maxObjectsIncrease = 3;         // ���������� �������� ��� ���������� ������ maxIncreaseInterval ������
    public float maxIncreaseInterval = 6f;     // �������� ������� ��� ���������� ������������� ���������� ��������

    public Color circleColor = Color.red;      // ���� �������� �����
    public float circleRadius = 100f;          // ������ �������� �����

    public Text infoText;                      // ������ �� UI Text ��� ����������� ����������

    private float spawnTimer = 0f;             // ������ ��� ������������ ������� ���������
    private float increaseTimer = 0f;          // ������ ��� ������������ ������� ���������� ���������� ��������
    private int currentMaxObjects;              // ������� ������������ ���������� ��������
    private Transform spawnContainer;          // ��������� ��� ���� ������������ ��������
    private Queue<GameObject> objectPool = new Queue<GameObject>(); // ��� ��������
    private List<GameObject> activeObjects = new List<GameObject>(); // ������ �������� ��������

    private void Start()
    {
        // �������� ���������� ��� ��������
        spawnContainer = new GameObject("SpawnContainer").transform;
        spawnContainer.SetParent(transform); // ������������� ��������� � �������� ��������� ������� ��� �������� ����������

        // ������������� ���� ��������
        InitializeObjectPool(initialNumberOfObjects);

        currentMaxObjects = initialNumberOfObjects;

        DrawCircle(); // ������ ���� ��� ����������� ������������� ������� ���������

        // ���������, ��� infoText ����������
        if (infoText == null)
        {
            Debug.LogError("UI Text �� ���������� � ������� ObjectSpawner!");
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        increaseTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnObjects(); // ��������� ��������
            spawnTimer = 0f; // ����� �������
        }

        if (increaseTimer >= maxIncreaseInterval)
        {
            IncreaseMaxObjects(); // ���������� ������������� ���������� ��������
            increaseTimer = 0f; // ����� �������
        }

        UpdateInfoText(); // ���������� ���������� ����
    }

    private void InitializeObjectPool(int initialSize)
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    private void SpawnObjects()
    {
        // ���������, ��� ������� ���������� �������� �� ��������� ������������ ����������
        int objectsToSpawn = Mathf.Min(currentMaxObjects - activeObjects.Count, objectPool.Count);

        for (int i = 0; i < objectsToSpawn; i++)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);

            Vector3 randomPosition = Random.insideUnitCircle * spawnRadius;
            randomPosition.z = randomPosition.y; // ��������� �������� �� Z, ����� ������������ ��� ��� ���������
            randomPosition.y = 0; // ������ ������� 0

            obj.transform.position = randomPosition;
            obj.transform.SetParent(spawnContainer); // ������������� ������������ ������ � ���������

            activeObjects.Add(obj);
        }
    }

    private void IncreaseMaxObjects()
    {
        currentMaxObjects += maxObjectsIncrease;

        // ���� �������� � ���� ������������, ��������� �����
        int additionalObjects = currentMaxObjects - objectPool.Count;
        if (additionalObjects > 0)
        {
            for (int i = 0; i < additionalObjects; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
        }
    }

    private void UpdateInfoText()
    {
        if (infoText != null)
        {
            // ���������� ������ � ������� � ������������ ����������� ��������
            int activeCount = activeObjects.Count;
            infoText.text = $"���������� �����������: {activeCount}/{currentMaxObjects}";
        }
    }

    private void DrawCircle()
    {
        GameObject circle = new GameObject("SpawnArea");
        LineRenderer lineRenderer = circle.AddComponent<LineRenderer>();

        lineRenderer.positionCount = 100;
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;

        lineRenderer.startColor = circleColor;
        lineRenderer.endColor = circleColor;

        float angleStep = 360f / lineRenderer.positionCount;
        float angle = 0f;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float x = circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
            angle += angleStep;
        }
    }
}
