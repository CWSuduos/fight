using System.Collections;
using UnityEngine;

public class TriangleMotion : MonoBehaviour
{
    public float speed = 5f;        // Скорость перемещения объекта
    public float duration = 3f;     // Время выполнения полного цикла треугольника
    public float disappearTime = 4f; // Время, через которое объект пропадает

    private Vector3[] points;       // Вершины треугольника
    private int currentPointIndex;  // Текущая вершина треугольника
    private float startTime;        // Время начала движения

    private void Start()
    {
        // Определяем вершины треугольника относительно позиции объекта
        points = new Vector3[3];
        points[0] = transform.position; // Первая вершина - начальная позиция объекта
        points[1] = points[0] + new Vector3(1, 0, 0); // Вторая вершина - справа от начальной позиции
        points[2] = points[0] + new Vector3(0.5f, 0, Mathf.Sqrt(0.75f)); // Третья вершина - выше по диагонали

        currentPointIndex = 0;
        startTime = Time.time;

        // Запускаем корутину для удаления объекта
        StartCoroutine(DisappearAfterTime(disappearTime));
    }

    private void Update()
    {
        // Вычисляем время, прошедшее с начала движения
        float elapsedTime = Time.time - startTime;
        float cycleTime = duration / 3f; // Время для перемещения между вершинами

        // Определяем следующую вершину
        int nextPointIndex = (currentPointIndex + 1) % points.Length;

        // Вычисляем прогресс между вершинами
        float progress = Mathf.PingPong(elapsedTime / cycleTime, 1f);
        transform.position = Vector3.Lerp(points[currentPointIndex], points[nextPointIndex], progress);

        // Проверяем, достигли ли мы следующей вершины
        if (progress >= 1f)
        {
            // Переходим к следующей вершине
            currentPointIndex = nextPointIndex;
            startTime = Time.time;
        }
    }

    // Корутина для удаления объекта через заданное время
    private IEnumerator DisappearAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject); // Удаляем объект
    }
}
