using CDG.Core.State;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;

    private PlayerMover _playerMover;
    private PlayerAttackState _playerAttack;
    private PlayerClimb _playerClimb;
    private PlayerAction _playerAction;


    private void Start()
    {
        _stateMachine = new StateMachine();

        _playerMover = new PlayerMover();
        _stateMachine.Initialize(_playerMover);
    }
}
