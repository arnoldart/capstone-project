using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IPlayerState
{
    private PlayerMovement _playerMovement;

    public WalkState(PlayerMovement playerMovement)
    {
        this._playerMovement = playerMovement;
    }


    public void Enter()
    {
        UpdateAnimationDirection();
    }

    public void Update()
    {
        _playerMovement.movement.x = Input.GetAxisRaw("Horizontal");
        _playerMovement.movement.y = Input.GetAxisRaw("Vertical");

        _playerMovement.MoveCharacter();
        UpdateAnimationDirection();
        
        if (_playerMovement.movement.sqrMagnitude == 0)
        {
            _playerMovement.stateMachine.ChangeState(new IdleState(_playerMovement));
        }

    }


    public void UpdateAnimationDirection()
    {
        if (_playerMovement.movement.y > 0)
        {
            _playerMovement.animator.SetFloat("direction", 1); // Walk Up
        }
        else if (_playerMovement.movement.y < 0)
        {
            _playerMovement.animator.SetFloat("direction", 2); // Walk Down
        }
        else if (_playerMovement.movement.x < 0)
        {
            _playerMovement.animator.SetFloat("direction", 3); // Walk Left
        }
        else if (_playerMovement.movement.x > 0)
        {
            _playerMovement.animator.SetFloat("direction", 4); // Walk Right
        }
    }

    public void Exit()
    {
        
    }
}
