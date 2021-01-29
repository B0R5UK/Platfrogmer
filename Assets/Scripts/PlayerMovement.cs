using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState {Idle = 0, Running = 1, Jumping = 2 , Falling = -1}

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public SpriteRenderer sr;
    [Header("Player variables")]
    public PlayerState currentState;
    public float playerSpeed;
    public float jumpForce;
    public float gravityMultiply = 2.5f;
    public float smallJumpGravityMultiply = 2f;
    public float jumpForgiveness;
    private bool updateAnimations;
    private float lastTimeGrounded;
    private bool canDoubleJump = false;
    [Header("Ground checking")]
    private bool isGrounded;
    [SerializeField]private Transform groundCheckerObject;
    public float checkerBoxWidth;
    [SerializeField]private LayerMask groundLayer;
    [Header("Shuriken")]
    [SerializeField] private Transform leftShurikenSpawn;
    [SerializeField] private Transform rightShurikenSpawn;
    [SerializeField] private Shuriken shurikenPrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        updateAnimations = true;
    }

    private void Update()
    {
        HorizontalMovement();
        Jump();
        ShurikenShoot();
        if(updateAnimations)
            UpdateAnimationValues();
    }
    
    void HorizontalMovement()
    {
        float input = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(input * playerSpeed, rb.velocity.y);

        if (input > 0)
            sr.flipX = false;
        else if (input < 0)
            sr.flipX = true;
    }

    void Jump()
    {
        GroundChecker();

        if (Input.GetButtonDown("Jump"))
        {
            bool rememberGrounded = Time.time - lastTimeGrounded < jumpForgiveness;

            if (isGrounded || rememberGrounded)
            {
                GameController.Instance.AudioController.PlaySound(SoundType.Jump);
                rb.AddForce(new Vector2(0, jumpForce));
            }
            else if (canDoubleJump)
            {
                GameController.Instance.AudioController.PlaySound(SoundType.Jump);
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpForce));
                canDoubleJump = false;
            }
        }

        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * gravityMultiply * Time.deltaTime;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            rb.velocity += Vector2.up * Physics2D.gravity.y * smallJumpGravityMultiply * Time.deltaTime;
    }

    void GroundChecker()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckerObject.position, new Vector2(checkerBoxWidth, 0f), 180f, groundLayer);

        if (isGrounded)
        {
            //rb.velocity = new Vector2(rb.velocity.x, 0);
            canDoubleJump = true;
            lastTimeGrounded = Time.time;
        }
    }

    void ShurikenShoot()
    {
        if (Input.GetButtonDown("Fire") && GameController.Instance.Player.canShoot)
        {
            GameController.Instance.AudioController.PlaySound(SoundType.Shuriken);
            if (!sr.flipX)           
                Instantiate(shurikenPrefab, rightShurikenSpawn.position, Quaternion.identity);            
            else            
                Instantiate(shurikenPrefab, leftShurikenSpawn.position, Quaternion.identity);         
        }
    }

    public void SpikesKnockback()
    {
        rb.velocity = new Vector2(0,15f);
    }

    void UpdateAnimationValues()
    {
        if (rb.velocity.x != 0)
            currentState = PlayerState.Running;
        if (rb.velocity.y > 1)
            currentState = PlayerState.Jumping;
        else if (rb.velocity.y < -1)
            currentState = PlayerState.Falling;
        if (rb.velocity.x.Equals(0) && isGrounded)
            currentState = PlayerState.Idle;
        GameController.Instance.Player.animator.SetInteger("State", (int)currentState);
    }

    public void FreezePlayer()
    {
        updateAnimations = false;
        rb.simulated = false;
        GameController.Instance.Player.animator.SetInteger("State", -2);
        rb.velocity = new Vector2(0, 0);       
    }

    public void StickPlayerToPlatform(Transform parent)
    {
        transform.parent = parent;
    }

    public void UnStickPlayerFromPlatform()
    {
        transform.parent = null;
    }
}
