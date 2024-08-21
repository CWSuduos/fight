using System.Collections;
using UnityEngine;

public class DestroyMagicObjects : MonoBehaviour
{
    public float delay = 9f;        // Время задержки перед удалением объекта
    public float growthDuration = 2f; // Время на увеличение и уменьшение размера
    public float scaleMultiplier = 10f; // Множитель для увеличения размера

    private Vector3 originalScale; // Исходный масштаб объекта

    private void Start()
    {
        // Сохраняем исходный масштаб
        originalScale = transform.localScale;

        // Запускаем корутину для удаления объекта с изменением размера
        StartCoroutine(HandleDestruction());
    }

    private IEnumerator HandleDestruction()
    {
        float halfGrowthDuration = growthDuration / 2f;
        float elapsedTime = 0f;

        // Увеличиваем размер объекта
        while (elapsedTime < halfGrowthDuration)
        {
            elapsedTime += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(1f, scaleMultiplier, elapsedTime / halfGrowthDuration);
            transform.localScale = originalScale * scaleFactor;
            yield return null;
        }

        // Поддерживаем максимальный размер на 2 секунды
        yield return new WaitForSeconds(delay - growthDuration);

        // Уменьшаем размер объекта обратно к исходному
        elapsedTime = 0f;
        while (elapsedTime < halfGrowthDuration)
        {
            elapsedTime += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(scaleMultiplier, 1f, elapsedTime / halfGrowthDuration);
            transform.localScale = originalScale * scaleFactor;
            yield return null;
        }

        // Удаляем объект
        Destroy(gameObject);
    }
}
