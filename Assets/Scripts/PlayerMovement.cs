using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xVelocity = 10.0f;
    public float yVelocity = 9.0f;
    public float rayCastLength = 0.85f;
    public bool isGrounded = false;
    public bool isIt = false;
    public static float timeSinceTagged = 0.0f;
    public float invulnerabilityTime = 3.0f;
    public int playerNum = 0;
    private string axis;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        switch (playerNum) {
            case 0:
                axis = "WASD Horizontal";
                break;
            case 1:
                axis = "Arrow Horizontal";
                break;
            case 2:
                axis = "IJKL Horizontal";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis(axis);
        
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, rayCastLength, LayerMask.GetMask("Ground"));
       
        rb.linearVelocity = new Vector2(moveInput * xVelocity, rb.linearVelocityY);
        
        if (isGrounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))) { 
            rb.linearVelocity = new Vector2(rb.linearVelocityX, yVelocity);
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
