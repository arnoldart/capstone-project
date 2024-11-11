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
        _playerMovement.animator.SetFloat("speed", 0);
        UpdateAnimationDirection();
    }

    public void Update()
    {
        _playerMovement.movement.x = Input.GetAxisRaw("Horizontal");
        _playerMovement.movement.y = Input.GetAxisRaw("Vertical");

        _playerMovement.MoveCharacter();
        UpdateAnimationDirection();

        // Jika tombol lari ditekan, pindah ke RunState
        if (Input.GetMouseButton(1))
        {
            // Debug.Log("RUnning");
            _playerMovement.stateMachine.ChangeState(new RunState(_playerMovement));
        }
        // Jika tidak ada input, kembali ke IdleState
        else if (_playerMovement.movement.sqrMagnitude == 0)
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
