using UnityEngine;

public class PlatformSmash : MonoBehaviour
{
    [SerializeField] private GameObject hitPoint;
    private Transform currentPosition;
   

    private void Awake()
    {
        currentPosition.position = hitPoint.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitPoint.transform.position += transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hitPoint.transform.position += currentPosition.position;
        }
    }

}
