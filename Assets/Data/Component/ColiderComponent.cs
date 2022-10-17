using UnityEngine;
using UnityEngine.Events;

public class ColiderComponent : MonoBehaviour
{
    [SerializeField] UnityEvent UnityEvent;
    [SerializeField] string tagName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagName))
        {
            UnityEvent?.Invoke();
        }
    }
}
