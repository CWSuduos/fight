using UnityEngine;
using UnityEngine.SceneManagement; // ��� ������ �� �������

public class MainMenuButton : MonoBehaviour
{
    // �����, ������� ���������� ��� ������� �� ������
    public void OnClick()
    {
        // ���������, ���� �� ����� �������� ���� � ������ ������
        // �������� "MainMenu" �� ��� ����� ����� �������� ����
        SceneManager.LoadScene("MainMenu");
    }
}
