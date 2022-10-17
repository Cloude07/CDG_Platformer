using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    bool isJump;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.TryGetComponent(out Rigidbody2D rigidbody2D);
            if (Input.GetKey(KeyCode.W) && isJump && rigidbody2D.velocity.y == 0)
            {
                isJump = false;
            }
            if (!isJump)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    rigidbody2D.velocity = new Vector2(0, speed);

                }
                else if (Input.GetKey(KeyCode.S))
                {
                    rigidbody2D.velocity = new Vector2(0, -speed);
                }
                else if (Input.GetButton("Jump"))
                {
                    isJump = true;
                    rigidbody2D.gravityScale = 7;
                }
                else
                {
                    rigidbody2D.velocity = new Vector2(0, 0);
                    rigidbody2D.gravityScale = 0;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.TryGetComponent(out Rigidbody2D rigidbody2D);
        rigidbody2D.gravityScale = 7;
        isJump = false;
    }
}
