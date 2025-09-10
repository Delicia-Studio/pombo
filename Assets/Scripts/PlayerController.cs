using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject poopPrefab;
    public Transform poopSpawnPoint;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.velocity = moveInput * moveSpeed;
        }
    }

    void DropPoop()
    {
        Instantiate(poopPrefab, poopSpawnPoint.position, Quaternion.identity);
    }
}
