using UnityEngine;

public interface ICanSee
{
  public bool IsSee(Transform outPoint,Transform hitPoint, LayerMask layerMask)
    {
        RaycastHit2D hit;
        return hit = Physics2D.Linecast(outPoint.transform.position, hitPoint.transform.position, layerMask);
    }
}
