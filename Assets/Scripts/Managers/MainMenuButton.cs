using UnityEngine;
using UnityEngine.SceneManagement; // Для работы со сценами

public class MainMenuButton : MonoBehaviour
{
    // Метод, который вызывается при нажатии на кнопку
    public void OnClick()
    {
        // Проверяем, есть ли сцена главного меню в списке билдов
        // Замените "MainMenu" на имя вашей сцены главного меню
        SceneManager.LoadScene("MainMenu");
    }
}
