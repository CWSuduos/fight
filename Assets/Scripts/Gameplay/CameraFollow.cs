using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;               // Ссылка на трансформ игрока
    public float distance = 10f;           // Расстояние камеры от игрока
    public float height = 5f;              // Высота камеры относительно игрока
    public float rotationSpeed = 5f;       // Скорость вращения камеры

    [Range(-89f, 89f)]
    public float minPitch = -30f;          // Минимальный угол наклона камеры
    [Range(-89f, 89f)]
    public float maxPitch = 30f;           // Максимальный угол наклона камеры

    private float currentYaw;              // Текущий угол поворота камеры по оси Y
    private float currentPitch;            // Текущий угол наклона камеры по оси X

    private void Start()
    {
        Cursor.visible = false;            // Скрыть курсор при старте
        Cursor.lockState = CursorLockMode.Locked; // Заблокировать курсор в окне игры
    }

    private void LateUpdate()
    {
        // Получаем текущие координаты мыши
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Обновляем углы поворота камеры в зависимости от движения мыши
        currentYaw += mouseX * rotationSpeed;
        currentPitch -= mouseY * rotationSpeed;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch); // Ограничиваем угол наклона камеры

        // Рассчитываем новую позицию камеры
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 position = player.position - (rotation * Vector3.forward * distance) + (Vector3.up * height);

        transform.position = position;
        transform.LookAt(player.position + Vector3.up * height);
    }
}
