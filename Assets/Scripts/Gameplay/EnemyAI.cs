using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;                  // Скорость движения противника
    public float attackRange = 1.5f;              // Радиус для атаки игрока
    public float detectionRadius = 20f;           // Радиус обнаружения игрока
    public Transform player;                      // Ссылка на трансформ игрока
    public float fixedHeight = 1f;                // Фиксированная высота, на которой должен находиться противник
    public float timeToAttack = 0.6f;             // Время, после которого игрок считается ударенным

    private Animator animator;                    // Ссылка на аниматор противника
    private Rigidbody rb;                         // Ссылка на Rigidbody противника
    private float playerInRangeTimer = 0f;       // Таймер для отслеживания времени нахождения игрока в радиусе атаки
    private PlayerHealth playerHealth;            // Ссылка на компонент PlayerHealth

    private void Start()
    {
        animator = GetComponent<Animator>();      // Получение компонента Animator
        rb = GetComponent<Rigidbody>();           // Получение компонента Rigidbody
        playerHealth = player.GetComponent<PlayerHealth>(); // Получение компонента PlayerHealth из объекта игрока
    }

    private void Update()
    {
        MaintainFixedHeight();    // Поддержание постоянной высоты
        FacePlayer();             // Поворот лицом к игроку

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();  // Движение к игроку
                playerInRangeTimer = 0f; // Сброс таймера, если игрок в радиусе обнаружения
            }
            else
            {
                playerInRangeTimer += Time.deltaTime;

                if (playerInRangeTimer >= timeToAttack)
                {
                    Debug.Log("Игрок был ударен"); // Сообщение в консоль

                    // Проверка наличия компонента PlayerHealth на объекте игрока
                    if (playerHealth != null)
                    {
                        // Вызов метода OnHit на объекте игрока
                        playerHealth.OnHit();
                    }

                    playerInRangeTimer = 0f; // Сброс таймера после атаки
                }

                AttackPlayer();       // Атака игрока
            }
        }
        else
        {
            // Воспроизведение базовой анимации стояния, если игрок вне радиуса обнаружения
            animator.SetBool("isMoving", false);
            animator.Play("Idle_B");
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Игнорируем повороты по оси Y, чтобы враг не наклонялся
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);

        animator.SetBool("isMoving", true);
        animator.Play("move_forward_A");
    }

    private void AttackPlayer()
    {
        animator.SetBool("isMoving", false);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("bk_knee_left_A"))
        {
            animator.Play("bk_knee_left_A");
        }
    }

    private void MaintainFixedHeight()
    {
        // Поддержание фиксированной высоты
        Vector3 position = transform.position;
        position.y = fixedHeight;  // Установка высоты на фиксированное значение
        transform.position = position;
    }
}
