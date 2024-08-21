using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    public GameObject buttonPrefab;    // Префаб кнопки, которая будет добавляться в ScrollView
    public Transform scrollViewContent; // Контейнер ScrollView для добавления кнопок

    private void Start()
    {
        // Создание кнопки и добавление её в ScrollView
        CreateSceneButton("GameScene");
    }

    // Метод для создания кнопки в ScrollView
    private void CreateSceneButton(string sceneName)
    {
        if (buttonPrefab == null || scrollViewContent == null)
        {
            Debug.LogError("Не назначены необходимые объекты.");
            return;
        }

        // Создание новой кнопки из префаба
        GameObject buttonObject = Instantiate(buttonPrefab, scrollViewContent);

        // Настройка текста кнопки
        Text buttonText = buttonObject.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.text = sceneName;
        }

        // Назначение метода для загрузки сцены на событие нажатия кнопки
        Button button = buttonObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => LoadScene(sceneName));
        }
    }

    // Метод для загрузки сцены
    private void LoadScene(string sceneName)
    {
        // Проверка, существует ли сцена в билд-сеттингах
        if (IsSceneInBuildSettings(sceneName))
        {
            // Загрузка сцены
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Сцена с именем '{sceneName}' не найдена в билд-сеттингах.");
        }
    }

    // Проверяет, существует ли сцена в билд-сеттингах
    private bool IsSceneInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneManager.GetSceneByBuildIndex(i).path;
            if (scenePath.EndsWith(sceneName + ".unity"))
            {
                return true;
            }
        }
        return false;
    }
}
