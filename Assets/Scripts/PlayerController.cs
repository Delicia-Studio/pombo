using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject poopPrefab;

    [Header("Componentes")]
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;

    [Header("Ajustes de Física")]
    public float fallMultiplier = 2.5f; // Multiplicador para a queda
    public float lowJumpMultiplier = 2f; // Para saltos curtos (se soltar o botão cedo)
    public float linearDragNoAr = 0.5f;
    public float gravityScale = 30f;

    public enum PlayerState
    {
        Idle,
        Walking,
        Flying
    }

    public PlayerState currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (rb == null)
        {
            Debug.LogError("O Pombo precisa de um Rigidbody2D!");
        }
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A / D
        float moveY = Input.GetAxisRaw("Vertical");   // S / W

        moveInput = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            DropPoop();
        }

        if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }

        switch (currentState)
        {
            case PlayerState.Idle:
                HandleIdle();
                break;
            case PlayerState.Walking:
                HandleWalking();
                break;
            case PlayerState.Flying:
                HandleFlying();
                break;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = moveInput * moveSpeed;
            ApplyBetterFall();
        }
    }

    void DropPoop()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x - 0.2f, transform.position.y - 0.2f, transform.position.z + 1);
        Instantiate(poopPrefab, spawnPosition, Quaternion.identity);
    }

    void HandleIdle()
    {
        if (Input.GetAxis("Horizontal") != 0) 
            ChangeState(PlayerState.Walking);
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            ChangeState(PlayerState.Flying);
            rb.gravityScale = 0;
        }
    }

    void HandleWalking()
    {
        if (Input.GetAxis("Horizontal") == 0) 
            ChangeState(PlayerState.Idle);
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            ChangeState(PlayerState.Flying);
            rb.gravityScale = 0;
        }
    }

    void HandleFlying()
    {
        if (Input.GetKeyUp(KeyCode.Space)) {
            ChangeState(PlayerState.Idle);
            rb.gravityScale = gravityScale;
        }
    }

    void ApplyBetterFall()
    {
        if (currentState != PlayerState.Flying)
        {
            // Se a velocidade Y for menor que 0, estamos a CAIR
            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    public void ChangeState(PlayerState newState)
    {
        currentState = newState;
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isFlying", false);

        switch (currentState)
        {
            case PlayerState.Idle:
                animator.SetBool("isIdle", true);
                break;
            case PlayerState.Walking:
                animator.SetBool("isWalking", true);
                break;
            case PlayerState.Flying:
                animator.SetBool("isFlying", true);
                break;
        }
    }
}
