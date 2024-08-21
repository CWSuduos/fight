using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Массив объектов для отображения
    public GameObject[] objectsToShow;
    // Массив объектов для скрытия
    public GameObject[] objectsToHide;

    // Массив кнопок для активации объектов
    public Button[] buttonsToShow;
    // Массив кнопок для деактивации объектов
    public Button[] buttonsToHide;

    private void Start()
    {
        // Привязка функций к кнопкам
        foreach (Button button in buttonsToShow)
        {
            button.onClick.AddListener(() => ToggleObjects(true));
        }

        foreach (Button button in buttonsToHide)
        {
            button.onClick.AddListener(() => ToggleObjects(false));
        }
    }

    // Функция для кнопок "Start"
    public void OnStartButtonClicked()
    {
        // Переключение на сцену игры
        SceneManager.LoadScene("LobbyScene"); // Замените "GameScene" на название вашей сцены
    }

    // Функция для кнопок "About"
    public void OnAboutButtonClicked()
    {
        // Переключение на сцену о авторах
        SceneManager.LoadScene("AboutScene"); // Замените "AboutScene" на название вашей сцены
    }

    // Функция для кнопок "Exit"
    public void OnExitButtonClicked()
    {
        // Закрытие приложения
        Application.Quit();
    }

    // Переключение видимости объектов
    private void ToggleObjects(bool show)
    {
        if (show)
        {
            // Включаем объекты из массива objectsToShow и выключаем объекты из массива objectsToHide
            SetObjectActivation(objectsToShow, true);
            SetObjectActivation(objectsToHide, false);
        }
        else
        {
            // Включаем объекты из массива objectsToHide и выключаем объекты из массива objectsToShow
            SetObjectActivation(objectsToShow, false);
            SetObjectActivation(objectsToHide, true);
        }
    }

    // Устанавливает активность объектов
    private void SetObjectActivation(GameObject[] objects, bool active)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(active); // Управление активностью объекта
        }
    }
}
