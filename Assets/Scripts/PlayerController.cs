using System.Runtime.InteropServices.WindowsRuntime;
using System.Timers;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 20;
    private Vector2 moveDir;

    [SerializeField] private float maxVelocity;
    private Vector2 velocity;
    public float jumpSpeed = 2;
    private bool isJumping;
    private bool isGrounded = true;
    private float jumpTime;
    public float maxJumpTime = 0.8f;
    public void StartController()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void UpdateController()
    {
        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, rb.linearVelocity.y);

        /*
        moveDir = InputSystem.actions["Move"].ReadValue<Vector2>();
        Debug.Log("HE");
        //Vector2 newPos = new Vector2((transform.position.x + velocity.x), transform.position.y);
        // Jumping
        if (InputSystem.actions["Jump"].triggered && GroundCheck())
        {
            isJumping = true;
            jumpTime = maxJumpTime;
        }
        */
    }

    public void FixedUpdateController()
    {
        /*
        // AD movement
        //velocity.x = moveDir.x != 0 ? maxVelocity : 0; //moveDir.x * speed * Time.deltaTime;

        if (moveDir.x > 0) velocity.x = maxVelocity;
        else if (moveDir.x < -0) velocity.x = -maxVelocity;
        else velocity.x = 0;

        Vector3 jumpVec = Vector3.zero;
        if (isJumping)
        {
            jumpVec.y += jumpSpeed * Time.deltaTime; //(2 * jumpSpeed / maxJumpTime) * Time.deltaTime;
            jumpTime -= Time.deltaTime;
            if (jumpTime < 0)
            {
                isJumping = false;
            }
        }
        //newPos.y += velocity.y;
        //transform.position = newPos;
        Debug.Log(velocity.x + " " + velocity.y + " " + jumpTime);
        //rb.AddForce(velocity);
        //velocity.y -= 9.8f * Time.deltaTime;
        //rb.linearVelocity += velocity;
        //rb.AddForce(jumpVec);
        Vector2 xVelocity = new Vector2(velocity.x, 0);
        rb.linearVelocity = xVelocity;
        */
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDir.x = context.ReadValue<Vector2>().x;
    }

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
}
