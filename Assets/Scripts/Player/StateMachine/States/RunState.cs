using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IPlayerState
{
    private PlayerMovement _playerMovement;

    public RunState(PlayerMovement playerMovement)
    {
        this._playerMovement = playerMovement;
    }
    
    public void Enter()
    {
    }

    public void Update()
    {
        
    }

    public void Exit()
    {
    }
}
