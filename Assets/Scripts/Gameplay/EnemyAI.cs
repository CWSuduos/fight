using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;                  // �������� �������� ����������
    public float attackRange = 1.5f;              // ������ ��� ����� ������
    public float detectionRadius = 20f;           // ������ ����������� ������
    public Transform player;                      // ������ �� ��������� ������
    public float fixedHeight = 1f;                // ������������� ������, �� ������� ������ ���������� ���������
    public float timeToAttack = 0.6f;             // �����, ����� �������� ����� ��������� ���������

    private Animator animator;                    // ������ �� �������� ����������
    private Rigidbody rb;                         // ������ �� Rigidbody ����������
    private float playerInRangeTimer = 0f;       // ������ ��� ������������ ������� ���������� ������ � ������� �����
    private PlayerHealth playerHealth;            // ������ �� ��������� PlayerHealth

    private void Start()
    {
        animator = GetComponent<Animator>();      // ��������� ���������� Animator
        rb = GetComponent<Rigidbody>();           // ��������� ���������� Rigidbody
        playerHealth = player.GetComponent<PlayerHealth>(); // ��������� ���������� PlayerHealth �� ������� ������
    }

    private void Update()
    {
        MaintainFixedHeight();    // ����������� ���������� ������
        FacePlayer();             // ������� ����� � ������

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer > attackRange)
            {
                MoveTowardsPlayer();  // �������� � ������
                playerInRangeTimer = 0f; // ����� �������, ���� ����� � ������� �����������
            }
            else
            {
                playerInRangeTimer += Time.deltaTime;

                if (playerInRangeTimer >= timeToAttack)
                {
                    Debug.Log("����� ��� ������"); // ��������� � �������

                    // �������� ������� ���������� PlayerHealth �� ������� ������
                    if (playerHealth != null)
                    {
                        // ����� ������ OnHit �� ������� ������
                        playerHealth.OnHit();
                    }

                    playerInRangeTimer = 0f; // ����� ������� ����� �����
                }

                AttackPlayer();       // ����� ������
            }
        }
        else
        {
            // ��������������� ������� �������� �������, ���� ����� ��� ������� �����������
            animator.SetBool("isMoving", false);
            animator.Play("Idle_B");
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // ���������� �������� �� ��� Y, ����� ���� �� ����������
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
        // ����������� ������������� ������
        Vector3 position = transform.position;
        position.y = fixedHeight;  // ��������� ������ �� ������������� ��������
        transform.position = position;
    }
}
