using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    private float _currentSpeed;
    public Vector2 lastMoveDirection { get; private set; }
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movement;

    public PlayerStateMachine stateMachine { get; private  set; }
    private PlayerCombat _playerCombat;

    private void Start()
    {
        stateMachine = GetComponent<PlayerStateMachine>();
        stateMachine.ChangeState(new WalkState(this));
        lastMoveDirection = Vector2.zero;
    }

    private void Update()
    {
        // Cek apakah pemain menekan tombol kanan mouse untuk berlari
        if(Input.GetMouseButton(1)) {
            _currentSpeed = runSpeed; // Set kecepatan berlari
        }else {
            _currentSpeed = walkSpeed; // Set kecepatan berjalan
        }

        // Ambil input pergerakan
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

         // Periksa jika pemain tidak bergerak untuk kembali ke IdleState
        if (movement.sqrMagnitude == 0)
        {
            stateMachine.ChangeState(new IdleState(this));
        }
        else if (movement.sqrMagnitude > 0)
        {
            // Update lastMoveDirection dengan arah pergerakan saat ini
            lastMoveDirection = movement.normalized;

            // Jika ada input pergerakan, ubah ke WalkState atau RunState
            // if (_currentSpeed == runSpeed)
                // stateMachine.ChangeState(new RunState(this));
            // else
                stateMachine.ChangeState(new WalkState(this));
        }
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
        rb.MovePosition(rb.position + movement.normalized * _currentSpeed * Time.fixedDeltaTime);
    }
}