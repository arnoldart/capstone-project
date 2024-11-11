// PlayerMovement.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movement;

    private PlayerStateMachine stateMachine;
    private PlayerCombat _playerCombat;

    private void Start()
    {
        stateMachine = GetComponent<PlayerStateMachine>();
        _playerCombat = GetComponent<PlayerCombat>();

        // Mulai dengan WalkState
        stateMachine.ChangeState(new WalkState(this));
    }

    private void Update()
    {
        // Jangan gerakkan karakter saat menyerang
        if (animator.GetBool("IsAttacking"))
            return; // Jangan gerakkan karakter saat menyerang

        // Input pergerakan
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Input untuk serangan
        if (Input.GetMouseButtonDown(0) && !animator.GetBool("IsAttacking"))
        {
            // Pass stateMachine to AttackState constructor
            stateMachine.ChangeState(new AttackState(_playerCombat, this, stateMachine));
        }
    }

    private void FixedUpdate()
    {
        // Terapkan pergerakan ke Rigidbody2D
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}