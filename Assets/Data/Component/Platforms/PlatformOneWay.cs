using CDG.Core.Player;
using System.Collections;
using UnityEngine;

public class PlatformOneWay : MonoBehaviour
{
    [SerializeField] PlayerMove player;
    private PlatformEffector2D PlatformEffector2D;

    private void Awake()
    {
        PlatformEffector2D = GetComponent<PlatformEffector2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerMove>();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (player.isPressFromPlatform == true)
        {
            PlatformEffector2D.rotationalOffset = 180;
            StartCoroutine(Wait());
        }
        else if(player.isPressFromPlatform == false)
            return;
    }


    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        PlatformEffector2D.rotationalOffset = 0;
    }
}
