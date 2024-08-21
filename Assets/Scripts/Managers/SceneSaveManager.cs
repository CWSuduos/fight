using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneSaveManager : MonoBehaviour
{
    public InputField sceneNameInputField; // Поле ввода для имени сцены
    public Button saveButton;              // Кнопка для сохранения сцены
    public GameObject saveButtonPrefab;    // Префаб кнопки для сохраненных сцен
    public Transform scrollContent;        // Контейнер для кнопок в ScrollView

    private List<string> savedScenes = new List<string>(); // Список сохранённых сцен

    private void Start()
    {
        saveButton.onClick.AddListener(SaveScene); // Привязка метода сохранения сцены к кнопке
    }

    // Метод для сохранения сцены
    public void SaveScene()
    {
        string sceneName = sceneNameInputField.text;

        if (!string.IsNullOrEmpty(sceneName) && !savedScenes.Contains(sceneName))
        {
            // Сохранение сцены (может быть реализовано в виде сериализации состояния сцены)
            savedScenes.Add(sceneName);
            CreateSaveButton(sceneName); // Создание кнопки для новой сохраненной сцены

            Debug.Log($"Сцена '{sceneName}' сохранена.");
        }
        else
        {
            Debug.Log("Некорректное или уже существующее имя сцены.");
        }
    }

    // Метод для создания кнопки, которая загружает сохраненную сцену
    private void CreateSaveButton(string sceneName)
    {
        GameObject buttonObj = Instantiate(saveButtonPrefab, scrollContent);
        Button button = buttonObj.GetComponent<Button>();
        Text buttonText = buttonObj.GetComponentInChildren<Text>();
        buttonText.text = sceneName;

        // Привязка события загрузки сцены к кнопке
        button.onClick.AddListener(() => LoadScene(sceneName));
    }

    // Метод для загрузки сохраненной сцены
    private void LoadScene(string sceneName)
    {
        if (savedScenes.Contains(sceneName))
        {
            // Загрузка сцены (требуется предварительное сохранение сцены с именем)
            SceneManager.LoadScene(sceneName);
            Debug.Log($"Сцена '{sceneName}' загружена.");
        }
        else
        {
            Debug.Log("Сцена не найдена.");
        }
    }
}
