using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Poop"))
        {
            Destroy(gameObject);
        }
    }
}
