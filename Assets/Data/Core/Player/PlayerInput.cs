using UnityEngine;


namespace CDG.Core.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerMove playerMove;
        bool IskeyUp, isKeyPress, isPauses;

        private void Start() => playerMove = GetComponent<PlayerMove>();

        private void Update()
        {
            if (!isPauses)
                InputPlayer();

        }
        public void InputPlayer()
        {
            if (Input.GetButton("Horizontal"))
                playerMove.MoveHorizontal();

            if (Input.GetButtonUp("Horizontal"))
            {

            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                playerMove.MoveFromPlatform(true);
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                playerMove.MoveFromPlatform(false);
            }

            if (Input.GetButtonDown("Jump"))
            {
                IskeyUp = false;
                isKeyPress = true;
                playerMove.Jump(IskeyUp, isKeyPress);
            }
            else if (Input.GetButtonUp("Jump"))
            {
                IskeyUp = true;
                isKeyPress = true;
                playerMove.Jump(IskeyUp, isKeyPress);
            }
            else
            {
                isKeyPress = false;
                IskeyUp = false;
            }

            if (Input.GetButtonUp("Action"))
                Debug.Log("Дейсткие");

            if (Input.GetButtonUp("Hit"))
            {
                Debug.Log("Hit");
                playerMove.Hit();
            }
        }
    }
}
