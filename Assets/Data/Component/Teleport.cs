using System.Collections;
using UnityEngine;

namespace CDG.Components
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] GameObject Player;
        [SerializeField] Transform point;
        public TeleportPoint[] teleportPoint;
        public void TeleportObject() => StartCoroutine(Teleporting()); 

        private void OnEnable()
        {
            foreach (var point in teleportPoint)
            {
                point.OnPointChange += TeleportPoint_OnPointChange;
            }
        }

        private void TeleportPoint_OnPointChange(Vector2 transfortPoint, TeleportPoint teleport)
        {
            for (int i = 0; i < teleportPoint.Length; i++)
            {
                teleportPoint[i] = teleport;
                point.position = new Vector2(transfortPoint.x, transfortPoint.y);
            }


        }

        private void OnDisable()
        {
            for (int i = 0; i < teleportPoint.Length; i++)
            {
                teleportPoint[i].OnPointChange -= TeleportPoint_OnPointChange;

            }
        }

        IEnumerator Teleporting()
        {
            var waitFading = true;
            Fader.instance.FadeIn(() => waitFading = false);

            while (waitFading)
                yield return null;

            Player.transform.position = point.position;

            waitFading = true;
            Fader.instance.FadeOut(() => waitFading = false);

            while (waitFading)
                yield return null;
        }


    }
}
