using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour
{
    // Массив объектов, которые нужно включать
    public GameObject[] objectsToEnable;

    // Массив объектов, которые нужно выключать
    public GameObject[] objectsToDisable;

    // Задержка между выключением и включением объектов
    public float delay = 0.5f;

    // Функция для выключения, а затем включения активности объектов
    public void DisableThenEnableObjects()
    {
        StartCoroutine(DisableAndEnableCoroutine());
    }

    // Коррутина для последовательного выключения и включения объектов
    private IEnumerator DisableAndEnableCoroutine()
    {
        // Сначала выключаем объекты
        DisableObjects();

        // Ждём задержку
        yield return new WaitForSeconds(delay);

        // Затем включаем объекты
        EnableObjects();
    }

    // Функция для включения активности объектов
    public void EnableObjects()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            SetObjectActive(obj, true);
        }
    }

    // Функция для выключения активности объектов
    public void DisableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            SetObjectActive(obj, false);
        }
    }

    // Устанавливает активность объекта
    private void SetObjectActive(GameObject obj, bool active)
    {
        obj.SetActive(active);
    }
}
