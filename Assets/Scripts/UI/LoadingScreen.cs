using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // Время ожидания перед переключением сцены (в секундах)
    public float loadingTime = 6.0f;

    // Имя сцены, на которую нужно переключиться после загрузки
    public string nextSceneName;

    // Текстовое поле для отображения процента загрузки
    public Text loadingText;

    // Ползунок для отображения прогресса загрузки
    public Slider loadingSlider;

    // Таймер для отсчета времени
    private float timer;

    // Текущая скорость загрузки
    private float loadSpeed;

    // Максимальное и минимальное значение для случайной скорости загрузки
    public float minLoadSpeed = 0.5f;
    public float maxLoadSpeed = 2.0f;

    void Start()
    {
        // Инициализация таймера
        timer = 0f;

        // Установка начальной скорости загрузки
        loadSpeed = Random.Range(minLoadSpeed, maxLoadSpeed);

        // Убедитесь, что текстовое поле не пустое
        if (loadingText != null)
        {
            loadingText.text = "Loading... 0%";
        }

        // Инициализация ползунка загрузки, если он существует
        if (loadingSlider != null)
        {
            loadingSlider.value = 0f;
        }
    }

    void Update()
    {
        // Увеличение таймера в зависимости от прошедшего времени и случайной скорости загрузки
        timer += Time.deltaTime * loadSpeed;

        // Периодически изменяем скорость загрузки для добавления случайности
        if (Random.Range(0f, 1f) < 0.1f) // 10% шанс изменения скорости на каждом кадре
        {
            loadSpeed = Random.Range(minLoadSpeed, maxLoadSpeed);
        }

        // Вычисление текущего процента загрузки
        float progress = Mathf.Clamp01(timer / loadingTime);

        // Обновление текстового поля
        if (loadingText != null)
        {
            loadingText.text = $"Loading... {progress * 100f:F0}%";
        }

        // Обновление ползунка загрузки
        if (loadingSlider != null)
        {
            loadingSlider.value = progress;
        }

        // Проверка, если таймер достиг времени загрузки
        if (timer >= loadingTime)
        {
            // Переключение на следующую сцену
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
