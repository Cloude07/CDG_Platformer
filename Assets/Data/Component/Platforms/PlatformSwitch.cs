using System.Collections;
using UnityEngine;

public class PlatformSwitch : MonoBehaviour
{
    [SerializeField] private BoxCollider2D[] boxCollider;
    [SerializeField] private SpriteRenderer[] spriteRender;
    [SerializeField] private float timeOut = 1.5f;
    bool IsTimer = true;

    private void Update()
    {
        if(IsTimer)
        StartCoroutine(StartSwitch());  
    }

    IEnumerator StartSwitch()
    {
        IsTimer = false;
        yield return new WaitForSeconds(timeOut);
        closedPlatform(spriteRender, boxCollider);
        IsTimer = true;
    }

    private void closedPlatform(SpriteRenderer[] render, BoxCollider2D[] boxColliders)
    {
        for (int i = 0; i < render.Length; i++)
        {
            render[i].enabled = !render[i].enabled;
        }

        for (int i = 0; i < boxColliders.Length; i++)
        {
            boxColliders[i].enabled = !boxColliders[i].enabled;
        }
    }

}
