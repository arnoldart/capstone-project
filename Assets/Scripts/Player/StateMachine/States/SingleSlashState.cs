using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSlashState: IPlayerState
{
    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;
    
    public SingleSlashState(PlayerMovement playerMovement)
    {
        this._playerMovement = playerMovement;
        _playerCombat = playerMovement.GetComponent<PlayerCombat>();
    }
    
    public void Enter()
    {
        // _playerMovement.animator.SetInteger("attackType", 1);
        _playerMovement.animator.SetTrigger("singleSlash");
        _playerCombat.PerformAttack();
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0)) // Ketika mouse dilepaskan, kembali ke Idle atau Walk
        {
            _playerMovement.stateMachine.ChangeState(new IdleState(_playerMovement));
        }
    }

    public void Exit()
    {
        // _playerMovement.animator.SetInteger("attackType", 1);
        _playerMovement.animator.SetTrigger("singleSlash");
    }
}
