using UnityEngine;
using UnityEngine.UI; // ��� ������ � UI
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public Text tutorialText; // ������ �� UI ������� Text ��� ����������� ���������
    public float messageDuration = 2f; // ����� ����������� ���������

    private void Start()
    {
        // ������ �������� ��� ������ ���������
        StartCoroutine(ShowTutorialMessages());
    }

    private IEnumerator ShowTutorialMessages()
    {
        // ����� ��������� � WASD ����������
        ShowMessage("Use WASD to move.");
        yield return new WaitForSeconds(messageDuration);

        // ����� ��������� � ���������� �������
        ShowMessage("LKM to cast a magic projectile.");
        yield return new WaitForSeconds(messageDuration);

        // ����� ��������� � ����
        ShowMessage("Z to cast a shield projectile.");
        yield return new WaitForSeconds(messageDuration);

        // ����� ��������� � ��������
        ShowMessage("Health: Check your health status.");
        yield return new WaitForSeconds(messageDuration);

        // ����� ��������� � ���������� ������
        ShowMessage("Enemies: Keep track of enemy count.");
        yield return new WaitForSeconds(messageDuration);

        // ����� ��������� � �������
        ShowMessage("Enjoy the game!");
        yield return new WaitForSeconds(messageDuration);

        // ������� ������ ����� ����������
        tutorialText.text = "";
    }

    private void ShowMessage(string message)
    {
        if (tutorialText != null)
        {
            tutorialText.text = message;
        }
    }
}
