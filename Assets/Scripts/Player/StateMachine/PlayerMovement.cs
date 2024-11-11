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

    public PlayerStateMachine stateMachine { get; private  set; }
    private PlayerCombat _playerCombat;

    private void Start()
    {
        stateMachine = GetComponent<PlayerStateMachine>();
        stateMachine.ChangeState(new WalkState(this));
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (movement.sqrMagnitude > 0f) 
        {
            MoveCharacter();
        }
    }
    
    public void MoveCharacter()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}