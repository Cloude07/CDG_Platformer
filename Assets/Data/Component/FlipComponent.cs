using UnityEngine;

namespace CDG.Components
{
    public class FlipComponent
    {
        public bool faceRight;
        public Transform Reflect(Vector2 direction, Transform transform)
        {
            if ((direction.x < 0 && !faceRight) || (direction.x > 0 && faceRight))
            {
                transform.localScale *= new Vector2(-1, 1);
                faceRight = !faceRight;
                return transform;
            }
            return null;
        }
    }
}
