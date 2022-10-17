using System.Collections;
using UnityEngine;

public class PlatformFalling : MonoBehaviour
{
    private Rigidbody2D rigidbody2;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private const float FALLING_DELLAY = 0.5f;
    private const float DESTROY_PLATFORM = 1f;
    private float startPositin;
    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPositin = transform.position.y;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallingPlatform());
        }
    }

    private IEnumerator FallingPlatform()
    {
        yield return new WaitForSeconds(FALLING_DELLAY);
        rigidbody2.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(DESTROY_PLATFORM);
        boxCollider2D.enabled=false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(5);
        rigidbody2.bodyType = RigidbodyType2D.Static;
        boxCollider2D.enabled = true;
        spriteRenderer.enabled = true;
        transform.position = new Vector2(transform.position.x, startPositin);
    }
}
