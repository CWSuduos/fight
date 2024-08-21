using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour
{
    // ������ ��������, ������� ����� ��������
    public GameObject[] objectsToEnable;

    // ������ ��������, ������� ����� ���������
    public GameObject[] objectsToDisable;

    // �������� ����� ����������� � ���������� ��������
    public float delay = 0.5f;

    // ������� ��� ����������, � ����� ��������� ���������� ��������
    public void DisableThenEnableObjects()
    {
        StartCoroutine(DisableAndEnableCoroutine());
    }

    // ��������� ��� ����������������� ���������� � ��������� ��������
    private IEnumerator DisableAndEnableCoroutine()
    {
        // ������� ��������� �������
        DisableObjects();

        // ��� ��������
        yield return new WaitForSeconds(delay);

        // ����� �������� �������
        EnableObjects();
    }

    // ������� ��� ��������� ���������� ��������
    public void EnableObjects()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            SetObjectActive(obj, true);
        }
    }

    // ������� ��� ���������� ���������� ��������
    public void DisableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            SetObjectActive(obj, false);
        }
    }

    // ������������� ���������� �������
    private void SetObjectActive(GameObject obj, bool active)
    {
        obj.SetActive(active);
    }
}
