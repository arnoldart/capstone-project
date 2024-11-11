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
        
    }

    public void Update()
    {
        _playerMovement.movement.x = Input.GetAxisRaw("Horizontal");
        _playerMovement.movement.y = Input.GetAxisRaw("Vertical");
        
        if (_playerMovement.movement.sqrMagnitude == 0)
        {
            _playerMovement.animator.SetFloat("direction", 0); // Idle
        }
        else if (_playerMovement.movement.y > 0)
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

        // _playerMovement.animator.SetFloat("Horizontal", _playerMovement.movement.x);
        // _playerMovement.animator.SetFloat("Vertical", _playerMovement.movement.y);
        // _playerMovement.animator.SetFloat("Speed", _playerMovement.movement.sqrMagnitude);
    }

    public void Exit()
    {
        
    }
}
