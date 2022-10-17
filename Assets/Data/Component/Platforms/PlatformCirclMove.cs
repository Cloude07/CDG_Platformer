using System;
using UnityEngine;

public class PlatformCirclMove : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float radius = 2f, angilarSpeed = 2f;
    private const string TAG = "Player";
    private float positionX, positionY, angle = 0;

    private void Update()
    {
        positionX = center.position.x + Mathf.Cos(angle) * radius;
        positionY = center.position.y + Mathf.Sin(angle) * radius;
        transform.position = new Vector2(positionX, positionY);
        angle = angle + Time.deltaTime * angilarSpeed;
        if (angle >= 2 * Mathf.PI) angle -= 2 * Mathf.PI;
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

