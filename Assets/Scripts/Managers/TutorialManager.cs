using UnityEngine;
using UnityEngine.UI; // Для работы с UI
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public Text tutorialText; // Ссылка на UI элемент Text для отображения сообщений
    public float messageDuration = 2f; // Время отображения сообщения

    private void Start()
    {
        // Запуск корутины для показа сообщений
        StartCoroutine(ShowTutorialMessages());
    }

    private IEnumerator ShowTutorialMessages()
    {
        // Показ сообщения о WASD управлении
        ShowMessage("Use WASD to move.");
        yield return new WaitForSeconds(messageDuration);

        // Показ сообщения о магическом снаряде
        ShowMessage("LKM to cast a magic projectile.");
        yield return new WaitForSeconds(messageDuration);

        // Показ сообщения о щите
        ShowMessage("Z to cast a shield projectile.");
        yield return new WaitForSeconds(messageDuration);

        // Показ сообщения о здоровье
        ShowMessage("Health: Check your health status.");
        yield return new WaitForSeconds(messageDuration);

        // Показ сообщения о количестве врагов
        ShowMessage("Enemies: Keep track of enemy count.");
        yield return new WaitForSeconds(messageDuration);

        // Показ сообщения о радости
        ShowMessage("Enjoy the game!");
        yield return new WaitForSeconds(messageDuration);

        // Скрытие текста после завершения
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
