using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Text healthText;
    public Text respawnText;
    public float respawnTime = 5f;

    private bool isRespawning = false;
    public Transform respawnTarget; // Точка, вокруг которой будет происходить респавн
    public float respawnRadius = 100f; // Радиус респавна

    private void Start()
    {
        currentHealth = maxHealth;

        if (respawnText != null)
        {
            respawnText.gameObject.SetActive(false);
        }

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isRespawning) return;

        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHealthUI();

        Debug.Log($"Игрок получил урон: {damage}. Текущее здоровье: {currentHealth}.");

        if (currentHealth <= 0)
        {
            StartCoroutine(RespawnCountdown());
        }
    }

    public void OnHit()
    {
        int damage = Random.Range(5, 16);
        TakeDamage(damage);
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}";
        }
    }

    private IEnumerator RespawnCountdown()
    {
        isRespawning = true;

        if (respawnText != null)
        {
            respawnText.gameObject.SetActive(true);

            float elapsedTime = 0f;
            while (elapsedTime < respawnTime)
            {
                float timeLeft = respawnTime - elapsedTime;
                respawnText.text = $"Respawning in: {timeLeft:F1}";
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            respawnText.gameObject.SetActive(false);
        }

        Respawn();

        isRespawning = false;
    }

    private void Respawn()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        Debug.Log("Игрок респавнится и восстанавливает здоровье!");

        if (respawnTarget != null)
        {
            // Генерация случайной позиции в радиусе от таргета
            Vector2 randomPos = Random.insideUnitCircle * respawnRadius;
            Vector3 respawnPosition = new Vector3(randomPos.x, 0, randomPos.y) + respawnTarget.position;

            // Перемещение игрока в новую позицию
            transform.position = respawnPosition;
        }
        else
        {
            Debug.LogWarning("Respawn target is not set.");
        }
    }
}
