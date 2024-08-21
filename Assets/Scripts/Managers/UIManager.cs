using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ������ �������� ��� �����������
    public GameObject[] objectsToShow;
    // ������ �������� ��� �������
    public GameObject[] objectsToHide;

    // ������ ������ ��� ��������� ��������
    public Button[] buttonsToShow;
    // ������ ������ ��� ����������� ��������
    public Button[] buttonsToHide;

    private void Start()
    {
        // �������� ������� � �������
        foreach (Button button in buttonsToShow)
        {
            button.onClick.AddListener(() => ToggleObjects(true));
        }

        foreach (Button button in buttonsToHide)
        {
            button.onClick.AddListener(() => ToggleObjects(false));
        }
    }

    // ������� ��� ������ "Start"
    public void OnStartButtonClicked()
    {
        // ������������ �� ����� ����
        SceneManager.LoadScene("LobbyScene"); // �������� "GameScene" �� �������� ����� �����
    }

    // ������� ��� ������ "About"
    public void OnAboutButtonClicked()
    {
        // ������������ �� ����� � �������
        SceneManager.LoadScene("AboutScene"); // �������� "AboutScene" �� �������� ����� �����
    }

    // ������� ��� ������ "Exit"
    public void OnExitButtonClicked()
    {
        // �������� ����������
        Application.Quit();
    }

    // ������������ ��������� ��������
    private void ToggleObjects(bool show)
    {
        if (show)
        {
            // �������� ������� �� ������� objectsToShow � ��������� ������� �� ������� objectsToHide
            SetObjectActivation(objectsToShow, true);
            SetObjectActivation(objectsToHide, false);
        }
        else
        {
            // �������� ������� �� ������� objectsToHide � ��������� ������� �� ������� objectsToShow
            SetObjectActivation(objectsToShow, false);
            SetObjectActivation(objectsToHide, true);
        }
    }

    // ������������� ���������� ��������
    private void SetObjectActivation(GameObject[] objects, bool active)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(active); // ���������� ����������� �������
        }
    }
}
