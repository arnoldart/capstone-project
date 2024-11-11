using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
   private IPlayerState _currentState;

   public void ChangeState(IPlayerState newState)
   {
      if (_currentState != null)
      {
         _currentState.Exit();
      }

      _currentState = newState;
      _currentState.Enter();
   }

   private void Update()
   {
      if (_currentState != null)
      {
         _currentState.Update();
      }
   }
}
