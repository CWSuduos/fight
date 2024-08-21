using System.Collections;
using UnityEngine;

public class TriangleMotion : MonoBehaviour
{
    public float speed = 5f;        // �������� ����������� �������
    public float duration = 3f;     // ����� ���������� ������� ����� ������������
    public float disappearTime = 4f; // �����, ����� ������� ������ ���������

    private Vector3[] points;       // ������� ������������
    private int currentPointIndex;  // ������� ������� ������������
    private float startTime;        // ����� ������ ��������

    private void Start()
    {
        // ���������� ������� ������������ ������������ ������� �������
        points = new Vector3[3];
        points[0] = transform.position; // ������ ������� - ��������� ������� �������
        points[1] = points[0] + new Vector3(1, 0, 0); // ������ ������� - ������ �� ��������� �������
        points[2] = points[0] + new Vector3(0.5f, 0, Mathf.Sqrt(0.75f)); // ������ ������� - ���� �� ���������

        currentPointIndex = 0;
        startTime = Time.time;

        // ��������� �������� ��� �������� �������
        StartCoroutine(DisappearAfterTime(disappearTime));
    }

    private void Update()
    {
        // ��������� �����, ��������� � ������ ��������
        float elapsedTime = Time.time - startTime;
        float cycleTime = duration / 3f; // ����� ��� ����������� ����� ���������

        // ���������� ��������� �������
        int nextPointIndex = (currentPointIndex + 1) % points.Length;

        // ��������� �������� ����� ���������
        float progress = Mathf.PingPong(elapsedTime / cycleTime, 1f);
        transform.position = Vector3.Lerp(points[currentPointIndex], points[nextPointIndex], progress);

        // ���������, �������� �� �� ��������� �������
        if (progress >= 1f)
        {
            // ��������� � ��������� �������
            currentPointIndex = nextPointIndex;
            startTime = Time.time;
        }
    }

    // �������� ��� �������� ������� ����� �������� �����
    private IEnumerator DisappearAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject); // ������� ������
    }
}
