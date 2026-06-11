using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    // Use Rigidbody2d to move
    public Rigidbody2D rb2d;
    // Which layers do raycast respect / look for
    public LayerMask layerMask;
    public float distanceCheckWall = 1;
    public float distanceCheckWallOffsetY = -0.5f;
    public float distanceCheckLedge = 1;
    //
    public SpriteRenderer spriteRenderer;
    public float patrolSpeedX = 5;
    public bool moveRight = true;
    //
    public Player player;
    public float playerChaseRadius = 3;
    public float chaseSpeedX = 7;

    void Update()
    {

        // How far is player from AI
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        if (distanceToPlayer <= playerChaseRadius)
        {
            Chase();
        }
        else
        {
            Patrol();
        }

        // Flip on X axis if we are NOT moving right
        spriteRenderer.flipX = !moveRight;
    }


    void Chase()
    {
        // is player x coords greater than ours ? move right
        moveRight = player.transform.position.x > this.transform.position.x;
        //
        float linearVelocityX = moveRight ? +chaseSpeedX : -chaseSpeedX;
        rb2d.linearVelocityX = linearVelocityX;

    }

    void Patrol()
    {
        
        // We will shoot ray to detect walls from centre of enemy
        Vector2 wallDetectedOrigin = transform.position;
        // Offset Y up or down
        wallDetectedOrigin.y += distanceCheckWallOffsetY;
        // If we are moving right, direction is right, if left, direction is left
        Vector2 wallDetectedDir = moveRight ? Vector2.right : Vector2.left;
        // Shoot ray from origin in direction to a max of distance against layers in layer mask only
        bool willHitWall = Physics2D.Raycast(wallDetectedOrigin, wallDetectedDir, distanceCheckWall, layerMask);
        Debug.DrawLine(wallDetectedOrigin, wallDetectedOrigin + wallDetectedDir * distanceCheckWall);

        // Calculate position in front of AI to move
        Vector2 ledgeDetectOffsetDir = moveRight ? Vector2.right : Vector2.left;
        Vector2 ledgeDetectOrigin = (Vector2)transform.position + ledgeDetectOffsetDir;
        // Shoot ray downwards
        Vector2 ledgeDetectDir = Vector2.down;
        // If raycast does not hit anything we will walk off ledge
        bool willWalkOffLedge = !Physics2D.Raycast(ledgeDetectOrigin, ledgeDetectDir, distanceCheckLedge, layerMask);
        Debug.DrawLine(ledgeDetectOrigin, ledgeDetectOrigin + ledgeDetectDir * distanceCheckLedge);


        if (willHitWall == true || willWalkOffLedge == true)
        {
            // Move right is not what currently is, invert or flip bool
            moveRight = !moveRight;
            // Flip on X axis if we are NOT moving right
            spriteRenderer.flipX = !moveRight;
        }

        // Move!
        // Calculate movement direction

        float linearVelocityX = moveRight ? +patrolSpeedX : -patrolSpeedX;
        rb2d.linearVelocityX = linearVelocityX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("hit");
            //Reset scene
            SceneManager.LoadScene("Level2");
        }
    }
}
