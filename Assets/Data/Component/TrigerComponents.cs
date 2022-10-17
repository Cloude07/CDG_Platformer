using UnityEngine;
using UnityEngine.Events;

public class TrigerComponents : MonoBehaviour
{
    [SerializeField] UnityEvent UnityEvent;
    [SerializeField] bool isExitTrrigerEvent;
    [SerializeField] UnityEvent UnityEvent1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            UnityEvent?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isExitTrrigerEvent == true)
        {
            UnityEvent1?.Invoke();
        }
    }
}
