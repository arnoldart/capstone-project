using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerMovement _playerMovement;
    private PlayerStateMachine _stateMachine;

    public IdleState(PlayerMovement playerMovement)
    {
        this._playerMovement = playerMovement;
    }
    public void Enter()
    {
        // _playerMovement.animator.SetFloat("direction", 0);
        _playerMovement.animator.SetFloat("speed", 0);
        _playerMovement.animator.SetFloat("lastMoveX", _playerMovement.lastMoveDirection.x);
        _playerMovement.animator.SetFloat("lastMoveY", _playerMovement.lastMoveDirection.y);
    }

    public void Update()
    {
        // _playerMovement.animator.SetFloat("directionX", _playerMovement.movement.x);
        // _playerMovement.animator.SetFloat("directionY", _playerMovement.movement.y);
        

        if (_playerMovement.movement.sqrMagnitude > 0f)
        {
            _playerMovement.stateMachine.ChangeState(new WalkState(_playerMovement));
        }
    }

    public void Exit()
    {
        
    }
}
