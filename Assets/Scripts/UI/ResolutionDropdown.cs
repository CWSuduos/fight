using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionDropdown : MonoBehaviour
{
    public Dropdown resolutionDropdown;   // Ссылка на ваш Dropdown
    public Toggle fullscreenToggle;       // Ссылка на Toggle для полноэкранного режима
    private List<Resolution> availableResolutions; // Список доступных разрешений

    void Start()
    {
        // Получаем все доступные разрешения для экрана
        availableResolutions = new List<Resolution>(Screen.resolutions);

        // Очищаем все текущие опции в Dropdown
        resolutionDropdown.ClearOptions();

        // Создаем список строк для отображения в Dropdown
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < availableResolutions.Count; i++)
        {
            Resolution resolution = availableResolutions[i];
            string option = resolution.width + " x " + resolution.height;
            options.Add(option);

            // Проверяем текущее разрешение экрана и сохраняем его индекс
            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Добавляем опции в Dropdown
        resolutionDropdown.AddOptions(options);
        // Устанавливаем текущее разрешение как выбранное
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Подписываем функции на изменение значений в Dropdown и Toggle
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
        fullscreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(fullscreenToggle.isOn); });

        // Инициализируем состояние Toggle в зависимости от текущего полноэкранного режима
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        // Устанавливаем выбранное разрешение
        Resolution resolution = availableResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        // Переключение полноэкранного режима
        Screen.fullScreen = isFullScreen;
    }
}
