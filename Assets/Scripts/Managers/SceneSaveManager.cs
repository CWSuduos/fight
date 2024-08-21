using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneSaveManager : MonoBehaviour
{
    public InputField sceneNameInputField; // ���� ����� ��� ����� �����
    public Button saveButton;              // ������ ��� ���������� �����
    public GameObject saveButtonPrefab;    // ������ ������ ��� ����������� ����
    public Transform scrollContent;        // ��������� ��� ������ � ScrollView

    private List<string> savedScenes = new List<string>(); // ������ ���������� ����

    private void Start()
    {
        saveButton.onClick.AddListener(SaveScene); // �������� ������ ���������� ����� � ������
    }

    // ����� ��� ���������� �����
    public void SaveScene()
    {
        string sceneName = sceneNameInputField.text;

        if (!string.IsNullOrEmpty(sceneName) && !savedScenes.Contains(sceneName))
        {
            // ���������� ����� (����� ���� ����������� � ���� ������������ ��������� �����)
            savedScenes.Add(sceneName);
            CreateSaveButton(sceneName); // �������� ������ ��� ����� ����������� �����

            Debug.Log($"����� '{sceneName}' ���������.");
        }
        else
        {
            Debug.Log("������������ ��� ��� ������������ ��� �����.");
        }
    }

    // ����� ��� �������� ������, ������� ��������� ����������� �����
    private void CreateSaveButton(string sceneName)
    {
        GameObject buttonObj = Instantiate(saveButtonPrefab, scrollContent);
        Button button = buttonObj.GetComponent<Button>();
        Text buttonText = buttonObj.GetComponentInChildren<Text>();
        buttonText.text = sceneName;

        // �������� ������� �������� ����� � ������
        button.onClick.AddListener(() => LoadScene(sceneName));
    }

    // ����� ��� �������� ����������� �����
    private void LoadScene(string sceneName)
    {
        if (savedScenes.Contains(sceneName))
        {
            // �������� ����� (��������� ��������������� ���������� ����� � ������)
            SceneManager.LoadScene(sceneName);
            Debug.Log($"����� '{sceneName}' ���������.");
        }
        else
        {
            Debug.Log("����� �� �������.");
        }
    }
}
