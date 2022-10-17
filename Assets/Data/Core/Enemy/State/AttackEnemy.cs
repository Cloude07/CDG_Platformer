using CDG.Core.State;
using UnityEngine;

public class AttackEnemy : State
{
    private Transform _hitPoint;
    private Enemy _enemy;
    private LayerMask _layerMaskEnemy;
    private int _damage;
    public AttackEnemy(Enemy enemy, int damage,  Transform hitPoint, LayerMask layerMaskEnemy)
    {
        _enemy = enemy;
        _damage = damage;
        _hitPoint = hitPoint;
        _layerMaskEnemy = layerMaskEnemy;
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
        Hit();
    }

    public void Hit()
    {
        RaycastHit2D hit = Physics2D.Linecast(_enemy.transform.position, _hitPoint.position, _layerMaskEnemy);

        if (hit)
        {
            hit.collider.TryGetComponent(out IDamageable idamageable);
            idamageable.ApplyDamage(_damage);
        }
        else
            return;
    }
}
