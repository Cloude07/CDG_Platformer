using UnityEngine;

public class PlatformMoveComponent : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float speed;
    private const string TAG = "Player";
    private int i = 0;


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, points[i].position) <= 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TAG))
        {
                collision.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TAG))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
