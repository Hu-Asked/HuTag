using System.Linq.Expressions;
using Unity.Collections;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedMultiplier = 2.0f;
    public float slowMuliplier = 0.75f;
    public float jumpMuliplier = 2.0f;
    public float PlayerSpeed = 10.0f;
    public float JumpForce = 9.0f;
    public float itSpeedMultiplier = 1.25f;
    public float fallMultiplier = 2.5f;
    public float rayCastLength = 0.85f;
    public float gravityScale = 1.5f;
    public bool isIt = false;
    public static float timeSinceTagged = 0.0f;
    public float invulnerabilityTime = 3.0f;
    public int playerNum = 0;
    private string axis;
    private KeyCode Up;
    private KeyCode Down;
    public bool doubleJump = false;
    private RaycastHit2D isGrounded;
    private RaycastHit2D permeableCheck;
    public bool wallLeft = false;
    public bool wallRight = false;
    public bool wallJump = false;
    private bool isCollisionIgnored = false;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        switch (playerNum)
        {
            case 0:
                axis = "WASD Horizontal";
                Up = KeyCode.W;
                Down = KeyCode.S;
                break;
            case 1:
                axis = "Arrow Horizontal";
                Up = KeyCode.UpArrow;
                Down = KeyCode.DownArrow;
                break;
            case 2:
                axis = "IJKL Horizontal";
                Up = KeyCode.I;
                Down = KeyCode.K;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xVelocity = PlayerSpeed;
        float yVelocity = JumpForce;
        float moveInput = Input.GetAxisRaw(axis);
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayCastLength, LayerMask.GetMask("Ground"));
        permeableCheck = Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.up, rayCastLength * 2f, LayerMask.GetMask("Ground"));
        wallLeft = Physics2D.Raycast(transform.position, Vector2.left, rayCastLength, LayerMask.GetMask("Ground"));
        wallRight = Physics2D.Raycast(transform.position, Vector2.right, rayCastLength, LayerMask.GetMask("Ground"));
        if (isGrounded.collider != null)
        {
            if (isGrounded.transform.gameObject.CompareTag("Slow"))
            {
                xVelocity *= slowMuliplier;
            }
            else if (isGrounded.transform.gameObject.CompareTag("Speed"))
            {
                xVelocity *= speedMultiplier;
            }
        }

        if ((moveInput > 0 && wallRight) || (moveInput < 0 && wallLeft))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        }
        else
        {
            if (!isIt)
            {
                rb.linearVelocity = new Vector2(moveInput * xVelocity, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(moveInput * xVelocity * itSpeedMultiplier, rb.linearVelocityY);
            }
        }
        if (isGrounded && Input.GetKey(Up))
        {
            if (isGrounded.collider != null && isGrounded.transform.gameObject.CompareTag("Jump"))
            {
                yVelocity *= jumpMuliplier;
            }
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, yVelocity);
            doubleJump = false;
            wallJump = false;
        }
        if(rb.linearVelocityY > 0 && rb.linearVelocityY < 3f) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocityY - 1f);
        }
        if (permeableCheck.collider != null && permeableCheck.transform.gameObject.CompareTag("Permeable"))
        {
            if (!Physics2D.GetIgnoreCollision(GetComponent<Collider2D>(), permeableCheck.collider) && permeableCheck.transform.position.y + 1.1f > transform.position.y)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), permeableCheck.collider, true);
            }
        }
        if (isGrounded.collider != null && isGrounded.transform.gameObject.CompareTag("Permeable"))
        {
            if (Physics2D.GetIgnoreCollision(GetComponent<Collider2D>(), isGrounded.collider) && !isCollisionIgnored && isGrounded.transform.position.y < transform.position.y)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), isGrounded.collider, false);
            }
            if (Input.GetKey(Down))
            {
                StartCoroutine(DisableCollisionTemporarily(isGrounded.collider));
            }
        }

        if (Input.GetKeyDown(Up) && !isGrounded && !wallJump && (wallLeft || wallRight))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, yVelocity);
            wallJump = true;
        }
        else if (Input.GetKeyDown(Up) && !isGrounded && !doubleJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, yVelocity);
            doubleJump = true;
        }
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += (fallMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
    }
    private IEnumerator DisableCollisionTemporarily(Collider2D platformCollider)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), platformCollider, true);
        isCollisionIgnored = true;
        yield return new WaitForSeconds(0.5f);
        isCollisionIgnored = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player") && isIt && Time.time - timeSinceTagged >= invulnerabilityTime)
        {
            obj.GetComponent<PlayerMovement>().isIt = true;
            isIt = false;
            timeSinceTagged = Time.time;
        }
    }
}
