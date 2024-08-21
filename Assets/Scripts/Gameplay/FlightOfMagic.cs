using UnityEngine;

public class FlightOfMagic : MonoBehaviour
{
    public float radius = 5f;          // Радиус круга
    public float speed = 1f;           // Скорость вращения
    public int numberOfCircles = 2;    // Количество кругов
    public float moveSpeedToEnemy = 5f; // Скорость движения к врагу
    public float fixedHeight = 1f;     // Фиксированная высота
    public float inactivityThreshold = 0.2f; // Время бездействия до изменения тега

    private float angle;              // Текущий угол
    private Vector3 startPosition;    // Начальная позиция объекта
    private bool hasCompletedCircles = false; // Флаг завершения кругов
    private Transform targetEnemy;    // Целевой враг для перемещения
    private float inactivityTimer = 0f; // Таймер для отслеживания бездействия

    private void Start()
    {
        // Сохранение начальной позиции с фиксированной высотой
        startPosition = new Vector3(transform.position.x, fixedHeight, transform.position.z);
        transform.position = startPosition;
    }

    private void Update()
    {
        if (!hasCompletedCircles)
        {
            // Выполнение кругового движения
            angle += speed * Time.deltaTime;
            float x = startPosition.x + Mathf.Cos(angle) * radius;
            float z = startPosition.z + Mathf.Sin(angle) * radius;
            transform.position = new Vector3(x, fixedHeight, z);

            // Проверка завершения кругового движения
            if (angle >= 2 * Mathf.PI * numberOfCircles)
            {
                angle = 0f;
                hasCompletedCircles = true;
                FindClosestEnemy(); // Поиск ближайшего врага
            }
        }
        else
        {
            // Перемещение к врагу
            if (targetEnemy != null)
            {
                MoveTowardsTarget(targetEnemy.position);
            }
        }

        // Проверка на бездействие
        CheckInactivity();
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;

        GameObject closestEnemy = enemies[0];
        float minDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                closestEnemy = enemy;
                minDistance = distance;
            }
        }

        targetEnemy = closestEnemy.transform;
    }

    private void MoveTowardsTarget(Vector3 targetPosition)
    {
        // Перемещение объекта к целевой позиции с фиксированной высотой
        Vector3 targetPositionWithFixedHeight = new Vector3(targetPosition.x, fixedHeight, targetPosition.z);
        Vector3 direction = (targetPositionWithFixedHeight - transform.position).normalized;
        transform.position += direction * moveSpeedToEnemy * Time.deltaTime;
    }

    private void CheckInactivity()
    {
        // Проверка на бездействие
        if (targetEnemy != null && Vector3.Distance(transform.position, targetEnemy.position) < 0.1f)
        {
            inactivityTimer += Time.deltaTime;

            // Если таймер превышает пороговое значение, изменяем тег
            if (inactivityTimer >= inactivityThreshold)
            {
                ChangeTagToDeadObject(); // Изменение тега и сообщение в консоль
            }
        }
        else
        {
            inactivityTimer = 0f; // Сброс таймера при перемещении
        }
    }

    private void ChangeTagToDeadObject()
    {
        // Устанавливаем новый тег для объекта
        gameObject.tag = "DeadObject";
        Debug.Log("Объект не перемещается и изменил тег на DeadObject");
        enabled = false; // Отключаем скрипт после изменения тега
    }
}
