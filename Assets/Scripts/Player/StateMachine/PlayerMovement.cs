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
    
    private float holdTime = 0f; // Waktu yang tombol ditekan
    private float maxHoldTimeForLightWeapon = 0.5f; // Durasi maksimum untuk light weapon
    private float maxHoldTimeForMediumWeapon = 1f; // Durasi maksimum untuk medium weapon

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

        if (Input.GetMouseButtonDown(0)) // Mouse button pertama ditekan
        {
            holdTime = 0f; // Reset hold time saat tombol ditekan
        }

        if (Input.GetMouseButton(0)) // Mouse button pertama ditahan
        {
            holdTime += Time.deltaTime; // Tambahkan durasi hold time

            // Tentukan jenis senjata berdasarkan waktu penahanan
            if (holdTime >= maxHoldTimeForMediumWeapon)
            {
                // Jika tombol ditekan lebih lama dari durasi untuk senjata medium
                // stateMachine.ChangeState(new HeavyWeaponState(this));
                Debug.Log("HeavyWeaponState");
            }
            else if (holdTime >= maxHoldTimeForLightWeapon)
            {
                // Jika tombol ditekan lebih lama dari durasi untuk senjata ringan
                // stateMachine.ChangeState(new MediumWeaponState(this));
                Debug.Log("MediumWeaponState");
            }
            else
            {
                // Jika tombol ditekan lebih sebentar, gunakan senjata ringan atau single slash
                // stateMachine.ChangeState(new LightWeaponState(this));
                Debug.Log("LightWeaponState");
            }
        }

        if (Input.GetMouseButtonUp(0)) // Mouse button pertama dilepaskan
        {
            // Tentukan kondisi saat tombol dilepaskan
            if (holdTime < maxHoldTimeForLightWeapon)
            {
                Debug.Log("SingleSlashState");
                stateMachine.ChangeState(new SingleSlashState(this)); // Single Slash jika tombol cepat dilepas
            }
            else
            {
                stateMachine.ChangeState(new IdleState(this)); // Kembali ke idle jika tombol dilepaskan setelah hold
            }
            holdTime = 0f; // Reset hold time setelah tombol dilepaskan
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