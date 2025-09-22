using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject poopPrefab;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (rb == null)
        {
            Debug.LogError("O Pombo precisa de um Rigidbody2D!");
        }

        // Desativa a gravidade, já que agora ele voa livre
        rb.gravityScale = 0;
    }

    private void Update()
    {
        // Captura entrada WASD
        float moveX = Input.GetAxisRaw("Horizontal"); // A / D
        float moveY = Input.GetAxisRaw("Vertical");   // S / W

        moveInput = new Vector2(moveX, moveY).normalized;

        // Cagar nos alvos com CTRL
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            DropPoop();
        }

        if (moveX > 0)
        {
            // Garante que o sprite não está espelhado (virado para a direita)
            spriteRenderer.flipX = false;
        }
        // Se o input for para a esquerda (menor que 0)
        else if (moveX < 0)
        {
            // Espelha o sprite no eixo X (virado para a esquerda)
            spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    void DropPoop()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x - 0.2f, transform.position.y - 0.2f, transform.position.z + 1);
        Instantiate(poopPrefab, spawnPosition, Quaternion.identity);
    }
}
