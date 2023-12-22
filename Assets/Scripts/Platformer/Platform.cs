using UnityEngine;

public class Platform : MonoBehaviour
{


    [SerializeField] private float jumpForce;
    [SerializeField] private StartGame startGame;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0)
        {
            if (startGame.GameStarted())
            {
                jumpForce = 12f;
            }
            else
            {
                jumpForce = 1f;
            }
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector2 vel = rb.velocity;
            vel.y = jumpForce;
            rb.velocity = vel;
        }
    }
}
