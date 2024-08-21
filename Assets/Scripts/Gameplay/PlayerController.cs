using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float smoothMovement = 0.1f;
    public Animator animator;
    public GameObject magicBoltPrefab;
    public GameObject shieldBoltPrefab;
    public float magicBoltSpeed = 20f;
    public float shieldBoltSpeed = 15f;
    public Text cooldownText;

    private Rigidbody rb;
    private Vector3 movement;
    private Vector3 targetVelocity;

    private float magicBoltCooldown = 7f;
    private float shieldBoltCooldown = 4f;

    private float magicBoltNextFireTime = 0f;
    private float shieldBoltNextFireTime = 0f;

    private float cooldownTime;
    private string currentBoltType = "";

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleInput();
        UpdateAnimation();

        RotateHeadTowardsCursor();

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= magicBoltNextFireTime)
        {
            CreateMagicBolt();
        }
        else if (Input.GetKeyDown(KeyCode.Z) && Time.time >= shieldBoltNextFireTime)
        {
            CreateShieldBolt();
        }

        UpdateCooldownText();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;
        targetVelocity = movement * moveSpeed;
    }

    private void Move()
    {
        Vector3 smoothedVelocity = Vector3.Lerp(rb.velocity, targetVelocity, smoothMovement);
        rb.velocity = smoothedVelocity;
    }

    private void UpdateAnimation()
    {
        bool isMovingForward = movement.z > 0.1f;
        bool isMovingBackward = movement.z < -0.1f;
        bool isMovingRight = movement.x > 0.1f;
        bool isMovingLeft = movement.x < -0.1f;

        animator.SetBool("isMovingForward", isMovingForward);
        animator.SetBool("isMovingBackward", isMovingBackward);
        animator.SetBool("isMovingRight", isMovingRight);
        animator.SetBool("isMovingLeft", isMovingLeft);
    }

    private void RotateHeadTowardsCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        float distance;

        if (playerPlane.Raycast(ray, out distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            targetPoint.y = transform.position.y;

            Vector3 direction = (targetPoint - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }

    private void CreateMagicBolt()
    {
        if (magicBoltPrefab != null)
        {
            Instantiate(magicBoltPrefab, transform.position + transform.forward + Vector3.up, Quaternion.identity);
            magicBoltNextFireTime = Time.time + magicBoltCooldown;
            currentBoltType = "MagicBolt";
        }
    }

    private void CreateShieldBolt()
    {
        if (shieldBoltPrefab != null)
        {
            Instantiate(shieldBoltPrefab, transform.position + transform.forward + Vector3.up, Quaternion.identity);
            shieldBoltNextFireTime = Time.time + shieldBoltCooldown;
            currentBoltType = "ShieldBolt";
        }
    }

    private void UpdateCooldownText()
    {
        float remainingTime = 0;
        if (currentBoltType == "MagicBolt")
        {
            remainingTime = Mathf.Max(magicBoltNextFireTime - Time.time, 0);
        }
        else if (currentBoltType == "ShieldBolt")
        {
            remainingTime = Mathf.Max(shieldBoltNextFireTime - Time.time, 0);
        }

        if (cooldownText != null)
        {
            cooldownText.text = $"{currentBoltType}\nCooldown: {remainingTime:F1}s";
        }
    }
}
