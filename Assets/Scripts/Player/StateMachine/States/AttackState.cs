using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IPlayerState
{
    // private PlayerCombat _playerCombat;
    // private float _attackHoldTime = 0f;
    //
    // public AttackState(PlayerCombat playerCombat)
    // {
    //     this._playerCombat = playerCombat;
    // }
    //
    // public void Enter()
    // {
    //    
    // }
    //
    // public void Update()
    // {
    //     // Cek input untuk jenis serangan
    //     if (Input.GetMouseButtonDown(0)) // Klik kiri
    //     {
    //         _attackHoldTime = 0f; // Reset hold time saat klik baru dimulai
    //     }
    //
    //     // if (Input.GetMouseButton(0)) // Menahan klik kiri
    //     // {
    //     //     _attackHoldTime += Time.deltaTime;
    //     //
    //     //     if (_attackHoldTime < 0.5f)
    //     //     {
    //     //         _playerCombat.PerformAttack("LightWeapon"); // Serangan ringan
    //     //     }
    //     //     else if (_attackHoldTime < 1f)
    //     //     {
    //     //         _playerCombat.PerformAttack("MediumWeapon"); // Serangan sedang
    //     //     }
    //     //     else
    //     //     {
    //     //         _playerCombat.PerformAttack("HeavyWeapon"); // Serangan berat
    //     //     }
    //     // }
    //
    //     if (Input.GetMouseButtonUp(0)) // Lepas klik kiri
    //     {
    //         _attackHoldTime = 0f; // Reset hold time setelah klik selesai
    //         _playerCombat.PerformAttack("SingleSlash"); // Serangan tunggal saat dilepas
    //     }
    //
    //     // if (Input.GetKeyDown(KeyCode.Q)) // Skyfall Blast
    //     // {
    //     //     _playerCombat.PerformAttack("SkyfallBlast");
    //     // }
    //     //
    //     // if (Input.GetKeyDown(KeyCode.E)) // Circle Blast
    //     // {
    //     //     _playerCombat.PerformAttack("CircleBlast");
    //     // }
    // }
    //
    // public void Exit()
    // {
    //     
    // }
    
    private PlayerCombat _playerCombat;
    private PlayerMovement _playerMovement;
    private PlayerStateMachine _stateMachine;
    private float _attackHoldTime = 0f;
    private float _attackDuration = 1f;  // Durasi serangan

    public AttackState(PlayerCombat playerCombat, PlayerMovement playerMovement, PlayerStateMachine stateMachine)
    {
        this._playerCombat = playerCombat;
        this._playerMovement = playerMovement;
        this._stateMachine = stateMachine;
    }

    public void Enter()
    {
        // Set animator untuk menunjukkan bahwa karakter sedang menyerang
        _playerMovement.animator.SetBool("IsAttacking", true);
        
        // Lakukan serangan langsung begitu masuk ke state
        _playerCombat.PerformAttack("SingleSlash");

        // Set timer untuk durasi serangan
        _attackHoldTime = 0f;
    }

    public void Update()
    {
        // Menjaga waktu serangan tetap berjalan
        _attackHoldTime += Time.deltaTime;

        // Jika durasi serangan selesai, kembali ke state pergerakan
        if (_attackHoldTime >= _attackDuration)
        {
            // Matikan animasi serangan dan kembali ke WalkState
            _playerMovement.animator.SetBool("IsAttacking", false);
            _stateMachine.ChangeState(new WalkState(_playerMovement));
        }

        // Cek input untuk klik kiri
        if (Input.GetMouseButtonUp(0)) // Lepas klik kiri
        {
            _attackHoldTime = 0f; // Reset waktu serangan setelah klik selesai
            _playerCombat.PerformAttack("SingleSlash"); // Lakukan serangan tunggal
        }
    }

    public void Exit()
    {
        // Ketika keluar dari AttackState, matikan animasi serangan
        _playerMovement.animator.SetBool("IsAttacking", false);
    }

}
