using UnityEngine;
using UnityEngine.UI;

public class SensitivityManager : MonoBehaviour
{
    public Slider sensitivitySlider; // Ссылка на Slider для регулировки чувствительности
    public Text sensitivityValueText; // Ссылка на Text для отображения значения чувствительности
 //public CameraController cameraController; // Ссылка на ваш скрипт CameraController

    private float sensitivity; // Переменная для хранения текущего значения чувствительности

    private void Start()
    {
        // Устанавливаем начальное значение и диапазон ползунка
        sensitivitySlider.minValue = 0.1f;
        sensitivitySlider.maxValue = 100f;
 //sensitivitySlider.value = cameraController.sensitivity; // Установите начальное значение, если требуется

        // Инициализируем текущее значение чувствительности
        sensitivity = sensitivitySlider.value;
        UpdateSensitivityText();

        // Добавляем обработчик изменения значения ползунка
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void OnSensitivityChanged(float value)
    {
        // Обновляем значение чувствительности
        sensitivity = value;
        UpdateSensitivityText();

        // Применяем новое значение чувствительности
        ApplySensitivity(value);
    }

    private void UpdateSensitivityText()
    {
        // Обновляем текстовое поле с текущим значением чувствительности
        sensitivityValueText.text = "Sensitivity: " + sensitivity.ToString("F2"); // Форматируем до двух знаков после запятой
    }

    private void ApplySensitivity(float value)
    {
        // Проверяем, что cameraController не равен null
       // if (cameraController != null)
        {
       //     cameraController.SetSensitivity(value);
        }
      //  else
        {
       //     Debug.LogWarning("CameraController is not assigned in the SensitivityManager.");
        }
    }
}
