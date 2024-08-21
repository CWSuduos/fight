using UnityEngine;
using UnityEngine.SceneManagement; // ��� ���������� �������
using UnityEngine.UI; // ��� ������ � UI

public class SceneTransition : MonoBehaviour
{
    public string sceneName = "GameScene"; // ��� �����, �� ������� ����� �������

    public void OnButtonClick()
    {
        // ��������, ���������� �� ����� � ����-���������
        if (IsSceneInBuildSettings(sceneName))
        {
            // ������� �� ��������� �����
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"����� � ������ '{sceneName}' �� ������� � ����-���������.");
        }
    }

    // ���������, ���������� �� ����� � ����-���������
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
