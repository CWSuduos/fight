using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;               // ������ �� ��������� ������
    public float distance = 10f;           // ���������� ������ �� ������
    public float height = 5f;              // ������ ������ ������������ ������
    public float rotationSpeed = 5f;       // �������� �������� ������

    [Range(-89f, 89f)]
    public float minPitch = -30f;          // ����������� ���� ������� ������
    [Range(-89f, 89f)]
    public float maxPitch = 30f;           // ������������ ���� ������� ������

    private float currentYaw;              // ������� ���� �������� ������ �� ��� Y
    private float currentPitch;            // ������� ���� ������� ������ �� ��� X

    private void Start()
    {
        Cursor.visible = false;            // ������ ������ ��� ������
        Cursor.lockState = CursorLockMode.Locked; // ������������� ������ � ���� ����
    }

    private void LateUpdate()
    {
        // �������� ������� ���������� ����
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // ��������� ���� �������� ������ � ����������� �� �������� ����
        currentYaw += mouseX * rotationSpeed;
        currentPitch -= mouseY * rotationSpeed;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch); // ������������ ���� ������� ������

        // ������������ ����� ������� ������
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 position = player.position - (rotation * Vector3.forward * distance) + (Vector3.up * height);

        transform.position = position;
        transform.LookAt(player.position + Vector3.up * height);
    }
}
