using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xVelocity = 10.0f;
    public float yVelocity = 9.0f;
    public float fallMultiplier = 2.5f;
    public float rayCastLength = 0.85f;
    public float gravityScale = 1.5f;
    public bool isGrounded = false;
    public bool isIt = false;
    public static float timeSinceTagged = 0.0f;
    public float invulnerabilityTime = 3.0f;
    public int playerNum = 0;
    private string axis;
    private KeyCode Up;
    private KeyCode Down;
    private bool doubleJump = false;

    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        switch (playerNum) {
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
        float moveInput = Input.GetAxisRaw(axis);
        
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayCastLength, LayerMask.GetMask("Ground"));
        rb.linearVelocity = new Vector2(moveInput * xVelocity, rb.linearVelocityY);
        if (isGrounded && Input.GetKey(Up)) { 
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, yVelocity);
            doubleJump = false;
        }
        if(Input.GetKeyDown(Up) && !isGrounded && !doubleJump) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, yVelocity);
            doubleJump = true;
        }
        if (rb.linearVelocity.y < 0) {
            rb.linearVelocity += (fallMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player") && isIt && Time.time - timeSinceTagged >= invulnerabilityTime) {
            obj.GetComponent<PlayerMovement>().isIt = true;
            isIt = false;
            timeSinceTagged = Time.time;
        }   
    }
}
