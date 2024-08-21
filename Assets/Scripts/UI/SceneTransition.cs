using UnityEngine;
using UnityEngine.SceneManagement; // Для управления сценами
using UnityEngine.UI; // Для работы с UI

public class SceneTransition : MonoBehaviour
{
    public string sceneName = "GameScene"; // Имя сцены, на которую нужно перейти

    public void OnButtonClick()
    {
        // Проверка, существует ли сцена в билд-сеттингах
        if (IsSceneInBuildSettings(sceneName))
        {
            // Переход на указанную сцену
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
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            if (scenePath.EndsWith(sceneName + ".unity"))
            {
                return true;
            }
        }
        return false;
    }
}
