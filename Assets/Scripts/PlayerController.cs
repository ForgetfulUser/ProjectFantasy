using System.Runtime.InteropServices.WindowsRuntime;
using System.Timers;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("Move")]
    public float moveSpeed = 20;
    private Vector2 moveDir;

    [Header("Jump")]
    public float jumpPower = 10;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.5f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 1;
    public float maxFallSpeed = 18;
    public float fallSpeedMulitiplier = 2;

    public void StartController()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
    }

    public void UpdateController()
    {
        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, rb.linearVelocity.y);
        GroundCheck();
        Gravity();
    }

    private void Gravity()
    {
        if(rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMulitiplier; // Fall increasingly faster
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed)); // Cap Player at Max Fallspeed
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDir.x = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumps remaining " + jumpsRemaining );
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                // Hold jump for full height
                Debug.Log("Jump");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                jumpsRemaining--;
            }
            else if (context.canceled)
            {
                // Release jump early for half height
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                jumpsRemaining--;
            }
        }
    }

    private void GroundCheck()
    {
        float angle = Quaternion.Angle(groundCheckPos.rotation, Quaternion.identity);

        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }

    /*
    private bool GroundCheck()
    {
        float dist = 0.5f;
        Vector2 startPos = transform.position;
        startPos.y -= 0.5f;
        Vector2 frontPos = startPos;
        Vector2 backPos = startPos;
        frontPos.x += 0.5f;
        backPos.x -= 0.5f;
        bool frontHit = Physics2D.Raycast(frontPos, Vector2.down, dist, LayerMask.GetMask("Ground"));
        bool backHit = Physics2D.Raycast(backPos, Vector2.down, dist, LayerMask.GetMask("Ground"));
        bool isGrounded = frontHit || backHit;
        Debug.DrawRay(frontPos, Vector2.down, Color.red, dist);
        Debug.DrawRay(backPos, Vector2.down, Color.red, dist);
        return isGrounded;
    }
    */

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
