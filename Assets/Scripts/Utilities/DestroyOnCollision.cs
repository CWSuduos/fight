using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public string targetTag = "Magic"; // Тег объекта, при столкновении с которым объект будет уничтожен

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, имеет ли объект, с которым произошло столкновение, нужный тег
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Уничтожаем объект, на котором висит скрипт
            Destroy(gameObject);
        }
    }
}
