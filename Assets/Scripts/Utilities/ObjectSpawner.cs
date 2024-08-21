using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject prefab;                  // Префаб, который будет генерироваться
    public float spawnRadius = 100f;           // Радиус области генерации объектов
    public float spawnInterval = 4f;           // Интервал генерации объектов в секундах
    public int initialNumberOfObjects = 25;    // Начальное количество объектов для генерации
    public int maxObjectsIncrease = 3;         // Количество объектов для увеличения каждые maxIncreaseInterval секунд
    public float maxIncreaseInterval = 6f;     // Интервал времени для увеличения максимального количества объектов

    public Color circleColor = Color.red;      // Цвет видимого круга
    public float circleRadius = 100f;          // Радиус видимого круга

    public Text infoText;                      // Ссылка на UI Text для отображения информации

    private float spawnTimer = 0f;             // Таймер для отслеживания времени генерации
    private float increaseTimer = 0f;          // Таймер для отслеживания времени увеличения количества объектов
    private int currentMaxObjects;              // Текущее максимальное количество объектов
    private Transform spawnContainer;          // Контейнер для всех заспавленных объектов
    private Queue<GameObject> objectPool = new Queue<GameObject>(); // Пул объектов
    private List<GameObject> activeObjects = new List<GameObject>(); // Список активных объектов

    private void Start()
    {
        // Создание контейнера для объектов
        spawnContainer = new GameObject("SpawnContainer").transform;
        spawnContainer.SetParent(transform); // Устанавливаем контейнер в качестве дочернего объекта для удобства управления

        // Инициализация пула объектов
        InitializeObjectPool(initialNumberOfObjects);

        currentMaxObjects = initialNumberOfObjects;

        DrawCircle(); // Рисуем круг для визуального представления области генерации

        // Убедитесь, что infoText установлен
        if (infoText == null)
        {
            Debug.LogError("UI Text не установлен в скрипте ObjectSpawner!");
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        increaseTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnObjects(); // Генерация объектов
            spawnTimer = 0f; // Сброс таймера
        }

        if (increaseTimer >= maxIncreaseInterval)
        {
            IncreaseMaxObjects(); // Увеличение максимального количества объектов
            increaseTimer = 0f; // Сброс таймера
        }

        UpdateInfoText(); // Обновление текстового поля
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
        // Убедитесь, что текущее количество объектов не превышает максимальное количество
        int objectsToSpawn = Mathf.Min(currentMaxObjects - activeObjects.Count, objectPool.Count);

        for (int i = 0; i < objectsToSpawn; i++)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);

            Vector3 randomPosition = Random.insideUnitCircle * spawnRadius;
            randomPosition.z = randomPosition.y; // Переносим значение по Z, чтобы использовать его для генерации
            randomPosition.y = 0; // Высота остаётся 0

            obj.transform.position = randomPosition;
            obj.transform.SetParent(spawnContainer); // Устанавливаем заспавленный объект в контейнер

            activeObjects.Add(obj);
        }
    }

    private void IncreaseMaxObjects()
    {
        currentMaxObjects += maxObjectsIncrease;

        // Если объектов в пуле недостаточно, добавляем новые
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
            // Обновление текста с текущим и максимальным количеством объектов
            int activeCount = activeObjects.Count;
            infoText.text = $"Количество Противников: {activeCount}/{currentMaxObjects}";
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
