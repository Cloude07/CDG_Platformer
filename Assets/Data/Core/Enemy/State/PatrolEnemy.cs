using CDG.Core.State;
using UnityEngine;

public class PatrolEnemy : State
{
    private bool _leftPoint;
    private float _speed;
    private Enemy _enemy;
    private float _point1, _point2;
    public bool faceRight;
    public PatrolEnemy(float speed, float point1, float point2, Transform hitPoint, Enemy enemy)
    {
        _speed = speed;
        _enemy = enemy;
        _point1 = point1;
        _point2 = point2;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        IsPatrol();
    }

    private void IsPatrol()
    {

        if (_leftPoint == true)
        {
            _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, new Vector2(_point1, _enemy.transform.position.y), _speed * Time.deltaTime);
            if (_enemy.transform.position.x == _point1)
            {
                _enemy.transform.localScale *= new Vector2(-1, 1);
                    faceRight = !faceRight;
                    _leftPoint = false;
            }
        }
        else if (_leftPoint == false)
            _enemy.transform.position = Vector2.MoveTowards(_enemy.transform.position, new Vector2(_point2, _enemy.transform.position.y), _speed * Time.deltaTime);
        if (_enemy.transform.position.x == _point2)
        {
            _enemy.transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
            _leftPoint = true;
        }
    }

}


