using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public string targetTag = "Magic"; // ��� �������, ��� ������������ � ������� ������ ����� ���������

    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ����� �� ������, � ������� ��������� ������������, ������ ���
        if (collision.gameObject.CompareTag(targetTag))
        {
            // ���������� ������, �� ������� ����� ������
            Destroy(gameObject);
        }
    }
}
