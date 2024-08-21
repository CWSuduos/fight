using System.Collections;
using UnityEngine;

public class DestroyMagicObjects : MonoBehaviour
{
    public float delay = 9f;        // ����� �������� ����� ��������� �������
    public float growthDuration = 2f; // ����� �� ���������� � ���������� �������
    public float scaleMultiplier = 10f; // ��������� ��� ���������� �������

    private Vector3 originalScale; // �������� ������� �������

    private void Start()
    {
        // ��������� �������� �������
        originalScale = transform.localScale;

        // ��������� �������� ��� �������� ������� � ���������� �������
        StartCoroutine(HandleDestruction());
    }

    private IEnumerator HandleDestruction()
    {
        float halfGrowthDuration = growthDuration / 2f;
        float elapsedTime = 0f;

        // ����������� ������ �������
        while (elapsedTime < halfGrowthDuration)
        {
            elapsedTime += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(1f, scaleMultiplier, elapsedTime / halfGrowthDuration);
            transform.localScale = originalScale * scaleFactor;
            yield return null;
        }

        // ������������ ������������ ������ �� 2 �������
        yield return new WaitForSeconds(delay - growthDuration);

        // ��������� ������ ������� ������� � ���������
        elapsedTime = 0f;
        while (elapsedTime < halfGrowthDuration)
        {
            elapsedTime += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(scaleMultiplier, 1f, elapsedTime / halfGrowthDuration);
            transform.localScale = originalScale * scaleFactor;
            yield return null;
        }

        // ������� ������
        Destroy(gameObject);
    }
}
