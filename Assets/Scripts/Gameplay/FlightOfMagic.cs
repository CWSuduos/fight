using UnityEngine;

public class FlightOfMagic : MonoBehaviour
{
    public float radius = 5f;          // ������ �����
    public float speed = 1f;           // �������� ��������
    public int numberOfCircles = 2;    // ���������� ������
    public float moveSpeedToEnemy = 5f; // �������� �������� � �����
    public float fixedHeight = 1f;     // ������������� ������
    public float inactivityThreshold = 0.2f; // ����� ����������� �� ��������� ����

    private float angle;              // ������� ����
    private Vector3 startPosition;    // ��������� ������� �������
    private bool hasCompletedCircles = false; // ���� ���������� ������
    private Transform targetEnemy;    // ������� ���� ��� �����������
    private float inactivityTimer = 0f; // ������ ��� ������������ �����������

    private void Start()
    {
        // ���������� ��������� ������� � ������������� �������
        startPosition = new Vector3(transform.position.x, fixedHeight, transform.position.z);
        transform.position = startPosition;
    }

    private void Update()
    {
        if (!hasCompletedCircles)
        {
            // ���������� ��������� ��������
            angle += speed * Time.deltaTime;
            float x = startPosition.x + Mathf.Cos(angle) * radius;
            float z = startPosition.z + Mathf.Sin(angle) * radius;
            transform.position = new Vector3(x, fixedHeight, z);

            // �������� ���������� ��������� ��������
            if (angle >= 2 * Mathf.PI * numberOfCircles)
            {
                angle = 0f;
                hasCompletedCircles = true;
                FindClosestEnemy(); // ����� ���������� �����
            }
        }
        else
        {
            // ����������� � �����
            if (targetEnemy != null)
            {
                MoveTowardsTarget(targetEnemy.position);
            }
        }

        // �������� �� �����������
        CheckInactivity();
    }

    private void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;

        GameObject closestEnemy = enemies[0];
        float minDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                closestEnemy = enemy;
                minDistance = distance;
            }
        }

        targetEnemy = closestEnemy.transform;
    }

    private void MoveTowardsTarget(Vector3 targetPosition)
    {
        // ����������� ������� � ������� ������� � ������������� �������
        Vector3 targetPositionWithFixedHeight = new Vector3(targetPosition.x, fixedHeight, targetPosition.z);
        Vector3 direction = (targetPositionWithFixedHeight - transform.position).normalized;
        transform.position += direction * moveSpeedToEnemy * Time.deltaTime;
    }

    private void CheckInactivity()
    {
        // �������� �� �����������
        if (targetEnemy != null && Vector3.Distance(transform.position, targetEnemy.position) < 0.1f)
        {
            inactivityTimer += Time.deltaTime;

            // ���� ������ ��������� ��������� ��������, �������� ���
            if (inactivityTimer >= inactivityThreshold)
            {
                ChangeTagToDeadObject(); // ��������� ���� � ��������� � �������
            }
        }
        else
        {
            inactivityTimer = 0f; // ����� ������� ��� �����������
        }
    }

    private void ChangeTagToDeadObject()
    {
        // ������������� ����� ��� ��� �������
        gameObject.tag = "DeadObject";
        Debug.Log("������ �� ������������ � ������� ��� �� DeadObject");
        enabled = false; // ��������� ������ ����� ��������� ����
    }
}
