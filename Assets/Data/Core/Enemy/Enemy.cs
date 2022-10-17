using CDG.Components;
using CDG.Core.State;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private StateMachine _stateMachine;
    [SerializeField] float _speed;
    [SerializeField] private float _point1, _point2;

    [SerializeField] private Transform _pointChase;

    private BoxCollider2D colider;
    [SerializeField] private LayerMask _layerMask;

    private PatrolEnemy patrolEnemy;
    private IdleEnemy idleEnemy;
    private ChaseEnemy chaseEnemy;
    private AttackEnemy attackEnemy;

    [Header("Hit")]
    [SerializeField] private Transform _hitPoint;
    [SerializeField] LayerMask _layerMaskEnemy;
    [SerializeField] int _damage;

    FlipComponent flip;

   public bool IsChase { get; set; }
   [SerializeField] private bool _IsAttack = false;

    private void Start()
    {
        colider = GetComponent<BoxCollider2D>();
        patrolEnemy = new PatrolEnemy(_speed, _point1, _point2, _hitPoint, this);
        _stateMachine = new StateMachine();
        chaseEnemy = new ChaseEnemy(_speed, _point1, _point2, _pointChase, _hitPoint, this);
        attackEnemy = new AttackEnemy( this,_damage, _hitPoint, _layerMaskEnemy);
        _stateMachine.Initialize(patrolEnemy);

    }

    private void Update()
    {
       _stateMachine.CurrentState.Update();
        var direction = _pointChase.position - transform.position;

    
        if (IsSee() && IsChase == true && _IsAttack == false)
        {
            _stateMachine.ChageState(chaseEnemy);
        }
        else if(IsChase == false && _IsAttack == false)
        {
            _stateMachine.ChageState(patrolEnemy);
        }

            
    }

    private bool IsSee()
    {
        RaycastHit2D hit;
        hit = Physics2D.Linecast(transform.position, _pointChase.transform.position, _layerMask);
        IsChase = true;
        return hit;
    }

    public void Attack(bool IsAttack)
    {
        if (IsAttack)
        {
            _IsAttack = true;
        _stateMachine.ChageState(attackEnemy);
        }
        else
        { 
            _IsAttack = false;
            _stateMachine.ChageState(chaseEnemy);
        }
    }

 
}
