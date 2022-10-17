using UnityEngine;
using CDG.Components;

namespace CDG.Core.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D rigidbody2d;
        private Vector2 direction;
        private float currentHorizontalSpeed;
        private FlipComponent flip = new FlipComponent();
        private PlayerAnimatorState playerAnimator;
        private Vector2 vecGravity;
        private float startingGravityScale;
        private float startingGrav;

        private enum Animator_State
        {
            Player_Idle,
            Player_Run,
            Player_Jump,
            Player_Fall,
            Player_Active,
            Player_Clemb
        }

        private void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            startingGrav = rigidbody2d.gravityScale;
            playerAnimator = GetComponent<PlayerAnimatorState>();
            vecGravity = new Vector2(0, -Physics2D.gravity.y);
          startingGravityScale =  rigidbody2d.gravityScale;
        }

        [Header("WALKING")]
        [SerializeField] private float _speedMove = 5;
        [SerializeField] private float _moveClamp = 13;
        [SerializeField] private float _apexBonus = 2;

        private float apexPoint; 
        public bool DontMove;
        public void MoveHorizontal()
        {
            if (!DontMove)
            {
                direction.x = Input.GetAxis("Horizontal");
                rigidbody2d.velocity = new Vector2(direction.x * _speedMove, rigidbody2d.velocity.y);
                currentHorizontalSpeed += direction.x * _speedMove * Time.deltaTime;
                currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed, -_moveClamp, _moveClamp);
                var apexBonus = Mathf.Sign(direction.x) * _apexBonus * apexPoint;
                currentHorizontalSpeed += apexBonus * Time.deltaTime;
                flip.Reflect(direction, transform);
            }
        }



        [Header("JUMPING")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpForceUp;
        [SerializeField] private float _fallMultipliers;
        private bool isNotJump;
        private float jumpCoucleTime = 0.2f;
        private float jumpCoucleTimeCounter;
        private float jumpBufferTime = 0.2f;
        private float jumpBufferTimeCounter;

        public void Jump(bool isKeyUp, bool isKeyPress)
        {
            if (isKeyPress)
            {
                isNotJump = false;
                jumpBufferTimeCounter = jumpBufferTime;

                if (jumpCoucleTimeCounter > 0f && isKeyUp == false && jumpBufferTimeCounter > 0f)
                {
                    rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, _jumpForce);
                    jumpBufferTimeCounter = 0;
                }
                else if (rigidbody2d.velocity.y > 0 && isKeyUp == true && jumpBufferTimeCounter > 0f)
                {
                    rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, _jumpForceUp);
                    jumpCoucleTimeCounter = 0;
                    jumpBufferTimeCounter = 0;
                }
                else
                {
                    jumpBufferTimeCounter -= Time.deltaTime;
                }
            }
        }


        private void JumpCoucle()
        {
            if (isGraunded)
            {
                jumpCoucleTimeCounter = jumpCoucleTime;
            }
            else
            {
                jumpCoucleTimeCounter -= Time.deltaTime;
            }
            if (rigidbody2d.velocity.y < 0)
            {
                rigidbody2d.velocity -= vecGravity * _fallMultipliers * Time.deltaTime;
            }
        }


        public bool isPressFromPlatform = false;
        public void MoveFromPlatform(bool IsPreesButton)
        {
            isPressFromPlatform = IsPreesButton;
            return;
        }

        [Header("Hit")]
        [SerializeField] private Transform _hitPoint;
        [SerializeField] private LayerMask _layerMaskEnemy;
        [SerializeField] private int _damage;
        public void Hit()
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, _hitPoint.position, _layerMaskEnemy);

            if (hit)
            {
                hit.collider.TryGetComponent(out IDamageable idamageable);
                idamageable.ApplyDamage(_damage);
            }
            else
                return;
        }

        public void Damageable()
        {
            if (flip.faceRight)
                rigidbody2d.AddForce(Vector2.right * 8, ForceMode2D.Impulse);
            else
                rigidbody2d.AddForce(Vector2.left * 8, ForceMode2D.Impulse);
                 rigidbody2d.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }

        [Header("Ledge")]
        private bool greenBox, redBox;
        private bool isGrabing;
        [SerializeField] private float _redXOffset, _redYOffset, _redXSize, _redYSize, _greenXOffset, _greenYOffset, _greenXSize, _greenYSize;

        private void LedgeGrabing()
        {
            if (greenBox && !redBox && !isGrabing && !isNotJump)
            {
                DontMove = true;
                isGrabing = true;
            }
            else if(!greenBox && !isGrabing)
            {
                isGrabing = false;
                DontMove = false;
            }

            if (isGrabing)
            {
                rigidbody2d.velocity = new Vector2(0f, 0f);
                rigidbody2d.gravityScale = 0f;
            }
        }

        public void ChangePos()
        {
            transform.position = new Vector2(transform.position.x + (0.8f * transform.localScale.x), transform.position.y + 2f);
            rigidbody2d.gravityScale = startingGrav;
            isGrabing = false;
            DontMove = false;
        }


        [Header("Checker")]
        [SerializeField] private float _groundedCheckRadius = 0.5f;
        [SerializeField] private LayerMask _groundedLayer, _ladderLayer, _Ledgelayer;
        [SerializeField] private Transform _groundCheckPosition, _ladderCheckPosition;
        private bool isGraunded;
        //ћетод дл€ проверок столкновений с окружением
        private void CheckSurroundings()
        {
            isNotJump = isGraunded = Physics2D.OverlapCircle(_groundCheckPosition.position, _groundedCheckRadius, _groundedLayer);
            greenBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (_greenXOffset * transform.localScale.x), transform.position.y + _greenYOffset), new Vector2(_greenXSize, _greenYSize), 0f, _Ledgelayer);
            redBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (_redXOffset * transform.localScale.x), transform.position.y + _redYOffset), new Vector2(_redXSize, _redYSize), 0f, _Ledgelayer);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(new Vector2(transform.position.x + (_redXOffset * transform.localScale.x), transform.position.y + _redYOffset), new Vector2(_redXSize, _redYSize));
            Gizmos.DrawSphere(_groundCheckPosition.position, _groundedCheckRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector2(transform.position.x + (_greenXOffset * transform.localScale.x), transform.position.y + _greenYOffset), new Vector2(_greenXSize, _greenYSize));
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_ladderCheckPosition.position, _groundedCheckRadius);
        }



        private void FixedUpdate()
        {
            CheckSurroundings();
            JumpCoucle();
            LedgeGrabing();
            LogicAnimation();
        }


        private void LogicAnimation()
        {
            if (!isGrabing)
            {
                if (rigidbody2d.velocity.y > 0.09f )
                    playerAnimator.ChangeAnimationState(Animator_State.Player_Jump.ToString());

                if (rigidbody2d.velocity.y < 0.09f && !isGraunded)
                    playerAnimator.ChangeAnimationState(Animator_State.Player_Fall.ToString());

                if (isGraunded)
                {
                    if (rigidbody2d.velocity.x != 0 && rigidbody2d.velocity.y == 0)
                        playerAnimator.ChangeAnimationState(Animator_State.Player_Run.ToString());
                    else if(rigidbody2d.velocity.x == 0 && rigidbody2d.velocity.y == 0)
                        playerAnimator.ChangeAnimationState(Animator_State.Player_Idle.ToString());
                }
            }
            else if (isGrabing)
            {
                playerAnimator.ChangeAnimationState(Animator_State.Player_Clemb.ToString());
            }
        }
    }
}
