using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    public GameObject buttonPrefab;    // ������ ������, ������� ����� ����������� � ScrollView
    public Transform scrollViewContent; // ��������� ScrollView ��� ���������� ������

    private void Start()
    {
        // �������� ������ � ���������� � � ScrollView
        CreateSceneButton("GameScene");
    }

    // ����� ��� �������� ������ � ScrollView
    private void CreateSceneButton(string sceneName)
    {
        if (buttonPrefab == null || scrollViewContent == null)
        {
            Debug.LogError("�� ��������� ����������� �������.");
            return;
        }

        // �������� ����� ������ �� �������
        GameObject buttonObject = Instantiate(buttonPrefab, scrollViewContent);

        // ��������� ������ ������
        Text buttonText = buttonObject.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.text = sceneName;
        }

        // ���������� ������ ��� �������� ����� �� ������� ������� ������
        Button button = buttonObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => LoadScene(sceneName));
        }
    }

    // ����� ��� �������� �����
    private void LoadScene(string sceneName)
    {
        // ��������, ���������� �� ����� � ����-���������
        if (IsSceneInBuildSettings(sceneName))
        {
            // �������� �����
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
            string scenePath = SceneManager.GetSceneByBuildIndex(i).path;
            if (scenePath.EndsWith(sceneName + ".unity"))
            {
                return true;
            }
        }
        return false;
    }
}
