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

    public Transform attackPoint; // Titik serangan
    public float attackPointOffset = 0.5f; // Jarak attack point dari pemain

    public PlayerStateMachine stateMachine { get; private set; }

    private float holdTime = 0f; // Waktu yang tombol ditekan
    private float maxHoldTimeForLightWeapon = 0.5f; // Durasi maksimum untuk light weapon
    private float maxHoldTimeForMediumWeapon = 1f; // Durasi maksimum untuk medium weapon

    private PlayerCombat _playerCombat;

    private void Start()
    {
        stateMachine = GetComponent<PlayerStateMachine>();
        stateMachine.ChangeState(new WalkState(this));
        lastMoveDirection = Vector2.down; // Default arah awal
    }

    private void Update()
    {
        // Cek apakah pemain menekan tombol kanan mouse untuk berlari
        if (Input.GetMouseButton(1))
        {
            _currentSpeed = runSpeed; // Set kecepatan berlari
        }
        else
        {
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
            stateMachine.ChangeState(new WalkState(this));
        }

        // Update posisi attack point
        UpdateAttackPointPosition();
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

    void UpdateAttackPointPosition()
    {
        if (attackPoint != null)
        {
             // Tentukan arah rotasi attack point berdasarkan lastMoveDirection
            Quaternion newRotation = Quaternion.identity;

            float x = lastMoveDirection.x;
            float y = lastMoveDirection.y;

            // Periksa arah dengan toleransi untuk mengakomodasi nilai diagonal
            if (Mathf.Approximately(x, 0) && y > 0) // Atas
            {
                newRotation = Quaternion.Euler(0, 0, 90);
            }
            else if (Mathf.Approximately(x, 0) && y < 0) // Bawah
            {
                newRotation = Quaternion.Euler(0, 0, -90);
            }
            else if (x < 0 && Mathf.Approximately(y, 0)) // Kiri
            {
                newRotation = Quaternion.Euler(0, 0, 180);
            }
            else if (x > 0 && Mathf.Approximately(y, 0)) // Kanan
            {
                newRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (x < 0 && y > 0) // Kiri Atas (Diagonal)
            {
                newRotation = Quaternion.Euler(0, 0, 135); // Sudut 135 derajat
            }
            else if (x < 0 && y < 0) // Kiri Bawah (Diagonal)
            {
                newRotation = Quaternion.Euler(0, 0, -135); // Sudut -135 derajat
            }
            else if (x > 0 && y > 0) // Kanan Atas (Diagonal)
            {
                newRotation = Quaternion.Euler(0, 0, 45); // Sudut 45 derajat
            }
            else if (x > 0 && y < 0) // Kanan Bawah (Diagonal)
            {
                newRotation = Quaternion.Euler(0, 0, -45); // Sudut -45 derajat
            }
            attackPoint.localRotation = newRotation;
        }
    }
}