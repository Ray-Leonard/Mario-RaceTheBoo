using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private CharacterController controller;
    [SerializeField, Min(0)] private float speed = 5f;
    private float maxSpeed = 9.5f;
    private float originalSpeed = 5;
    [SerializeField, Min(0)] private float rotationSpeed = 10f;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField, Min(0)] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField, Min(0)] private float jumpHeight = 2f;

    private Vector3 movement;
    private Vector3 gravitationalForce;
    private bool isGrounded;
    private bool jumpMomentumCheck;

    [SerializeField] float CoinBoostUpStep;
    private PlayerStatus status;

    // animations
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource jumpSound;

    private bool isRunning;
    private bool isJump;
    private bool isPunch;

    private void Awake()
    {
        // max speed = 7
        status = GetComponent<PlayerStatus>();
        CoinBoostUpStep = (maxSpeed - originalSpeed) / status.GetMaxCoin();
    }

    private void Update()
    {
        Movement();
        UpdateAnimation();
        Punch();
        BoostUp();
    }

    private void BoostUp()
    {
        speed = originalSpeed + CoinBoostUpStep * status.GetCoinCount();
        anim.speed = 1 + status.GetCoinCount() * (0.6f / status.GetMaxCoin());
    }

    private void Movement()
    {
        // calculate movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;
        movement = (right * horizontal + forward * vertical).normalized;

        // check if player is trying to move
        if (movement != Vector3.zero)
        {
            // look in the direction of the movement
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime);
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // check if mario is grounded
        isGrounded = false;
        Collider[] hitColliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround);
        for (int i = 0; i < hitColliders.Length; ++i)
        {
            if (hitColliders[i].gameObject == this.gameObject)
                continue;

            if (!hitColliders[i].isTrigger)
            {
                isGrounded = true;
                break;
            }
        }

        jumpMomentumCheck = jumpMomentumCheck && Input.GetButton("Jump") && !isGrounded;

        // simulate gravity
        if (isGrounded)
        {
            // mario is standing on ground
            gravitationalForce.y = gravity * Time.deltaTime;
            jumpMomentumCheck = true;
            isJump = false;
        }
        else
        {
            // mario is in the air
            if (!jumpMomentumCheck && gravitationalForce.y > 0)
                gravitationalForce.y = 0;
            else
            {
                gravitationalForce.y += gravity * Time.deltaTime;
            }
        }

        // jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            gravitationalForce.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
            isJump = true;
            jumpSound.Play();
        }



        // move mario
        controller.Move((movement * speed * Time.deltaTime) + (gravitationalForce * Time.deltaTime));
    }

    private void Punch()
    {
        if(!isJump && !isRunning)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isPunch = true;
            }
        }
    }

    private void UpdateAnimation()
    {
        anim.SetBool("Jump", isJump);
        anim.SetBool("Run", isRunning);
        if (isPunch)
        {
            anim.SetTrigger("Punch");
            isPunch = false;
        }

    }



    private void OnCollisionEnter(Collision collision)
    {
        print(collision.collider.gameObject.name);
    }
}
