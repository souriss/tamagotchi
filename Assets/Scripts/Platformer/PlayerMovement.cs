using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rigitbody;
    [SerializeField] private bool _lookRight;
    [SerializeField] private Platform_Generator platformGenerator;
    [SerializeField] private StartGame startGame;


    private void Update()
    {
        if (startGame.GameStarted())
        {
            Move();
        }
    }

    private void Move()
    {
        _rigitbody.velocity = new Vector2(Input.acceleration.x * _moveSpeed, _rigitbody.velocity.y);
        CheckFlip();
    }

    private void CheckFlip()
    {
        if (Input.acceleration.x > 0 && !_lookRight)
        {
            Flip();
        }
        else if (Input.acceleration.x < 0 && _lookRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        _lookRight = !_lookRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platformGenerator.IncreasePlatformCount();
        }
    }


}