using System;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    TeleportPoint teleportPoint;
    public event Action<Vector2, TeleportPoint> OnPointChange;
    private void Start() => teleportPoint = GetComponent<TeleportPoint>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 vector2 = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
            OnPointChange?.Invoke(vector2, teleportPoint);
        }
    }
}
