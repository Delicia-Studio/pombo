using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Poop"))
        {
            GameManager.Instance.AddScore(scoreValue);
            Destroy(collision.gameObject); // remove a caca
        }
    }
}
