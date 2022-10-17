using CDG.Core.State;
using UnityEngine;

public class ChaseEnemy : State
{
    private bool _leftPoint;
    private float _speed, _currentSpeed;
    private Enemy _enemy;
    private float _point1, _point2;
    Transform _chasePoint;
    Transform _hitPoint;
    private LayerMask _layerMask = 7;
    StateMachine stateMachine = new StateMachine();
    private ChaseEnemy chaseEnemy;
    bool faceRight;
    public ChaseEnemy(float speed, float point1, float point2, Transform chasePoint, Transform hitPoint, Enemy enemy)
    {
        _speed = speed;
        _currentSpeed = speed;
        _enemy = enemy;
        _point1 = point1;
        _point2 = point2;
        _chasePoint = chasePoint;
        _hitPoint = hitPoint;
    }
    public override void Enter()
    {
        base.Enter();
        _chasePoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        Chase();
        //Debug.Log("Player" + _chasePoint.transform.position.x);
        //Debug.Log("Enemy" + _hitPoint.transform.position.x);
    }

    private void Chase()
    {
        _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, _chasePoint.transform.position, _currentSpeed * Time.deltaTime);
        if (_chasePoint.transform.position.x + 0.9f == _hitPoint.transform.position.x)
        {
            Debug.Log("0");

        }
        else if (_chasePoint.transform.position.x < _hitPoint.transform.position.x)
        {
            _leftPoint = true;
            Debug.Log("1");
            flip();
        }
        else if(_chasePoint.transform.position.x > _hitPoint.transform.position.x)
        {
            _leftPoint = false;
            Debug.Log("2");
            flip();
        }

        _currentSpeed = _speed;

    }

    private void flip()
    {
        if(_leftPoint)
        _enemy.transform.localScale = new Vector2(1,_enemy.transform.localScale.y);
        else
            _enemy.transform.localScale = new Vector2(-1, _enemy.transform.localScale.y);

    }

 
}
