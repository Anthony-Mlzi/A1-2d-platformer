using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // VARIABLES
    // We want to know about the player's Rigidbody2D component to add forces to it
    public Rigidbody2D rb2d;
    // Get the player's collider / shape
    public CapsuleCollider2D capsuleCollider;
    // We want the player's animator component to synchronize its staes to player movement
    public Animator animator;
    // We want to flip the player on the X axis
    public SpriteRenderer spriteRenderer;
    // How fast do we want the player to move 
    public float speedX = 3f;
    //
    public float jumpSpeed = 3f;
    public float jumpTime = 0.300f; // In seconds
    public float maxCoyoteTime = 0.100f; // In seconds
    public float jumpTimeRemaining = 0.300f;
    public bool isJumping;
    public float maxJumpTime = 0.300f;
    public float gravScale = -10;
    //
    public LayerMask groundLayer;
    //
    public float raycastDistance = 0.05f;
    //

    //
    private float coyoteTimeRemaining;

    public WateringCan MostImportantWateringCan;

    // Physics and raycast variables
    Vector2 edgeClipTopOrigin;
    Vector2 edgeClipBottomOrigin;
    Vector2 edgeClipRayDistance;

    void Start()
    {
        // Easy way to get the reference to the two components
        // GetComponent asks this GameObject for the variable type
        // It returns the first one it finds, null if not attached
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        Physics2D.gravity = new Vector2(0, gravScale);

        ////////////////////////////////////////////////////////////////////////////////
        /// MOVE HORIZONTAL
        // Get the player's movement input from Unity's legacy input system
        float moveX = Input.GetAxis("Horizontal");
        // Math.Abs() gives is the number's absolute value
        // Abs(+1) and Abs(-1) both give us +1

        bool isMovingHorizontally = Mathf.Abs(moveX) > 0.1f;
        if (isMovingHorizontally)
        {

            // Move X is negative, ie. moving left
            bool isFacingLeft = moveX < 0f;
            spriteRenderer.flipX = isFacingLeft;

            // Check to see if player is hitting a wall horizontally
            Vector2 centre = transform.position;
            Vector2 extents = capsuleCollider.bounds.extents;
            float extentsX = isFacingLeft ? -extents.x : +extents.x;
            edgeClipTopOrigin = centre + new Vector2(extentsX, +extents.y);
            edgeClipBottomOrigin = centre + new Vector2(extentsX, -extents.y * 0.05f);
            Vector2 direction = Vector2.Normalize(new Vector2(extentsX, 0));
            edgeClipRayDistance = direction * raycastDistance;
            bool hitTop = Physics2D.Raycast(edgeClipTopOrigin, direction, raycastDistance, groundLayer);
            bool hitbottom = Physics2D.Raycast(edgeClipBottomOrigin, direction, raycastDistance, groundLayer);
            if (hitTop == false && hitbottom is false)
            {

                // Set move speed (horizontal) directly
                rb2d.linearVelocityX = moveX * speedX;

            }
            Debug.DrawLine(edgeClipTopOrigin, edgeClipTopOrigin + edgeClipRayDistance, hitTop ? Color.red : Color.green); 
            Debug.DrawLine(edgeClipBottomOrigin, edgeClipBottomOrigin + edgeClipRayDistance, hitbottom ? Color.red : Color.green);


        }
        // Sync the animator's parameters to the player's movement so it may
        // automatically control the player's animation
        animator.SetFloat("moveSpeedX", Mathf.Abs(moveX));

        //////////////////////////////////////////////////////////////////////////////////////////
        /// JUMP

        // Additional gravity while falling

        if (rb2d.linearVelocityY < 0f)
        {
            rb2d.AddForceY(gravScale * 0.1f);
        }

        // Decrement coyote time timer
        coyoteTimeRemaining -= Time.deltaTime;

        Vector2 rayOrigin = this.transform.position;
        Vector2 rayDirection = Vector2.down;
        float distance = 1.05f;
        bool isGrounded = Physics2D.Raycast(rayOrigin, rayDirection, distance, groundLayer);
        if (isGrounded)
        {
            // Reset coyote time timer when on ground
            coyoteTimeRemaining = maxCoyoteTime;
        }

        // Can we jump
        if (isGrounded == true || coyoteTimeRemaining > 0)
        {
            // Is jump key pressed this frame
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Remove ability to coyote jump
                coyoteTimeRemaining = 0;
                // Add force in Y axis
                isJumping = true;
                jumpTimeRemaining = maxJumpTime;
            }
        }

        // If we can continue holding down jump
        if (jumpTimeRemaining > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb2d.linearVelocityY = jumpSpeed;
            }
            else
            {
                jumpTimeRemaining = 0;
            }
            jumpTimeRemaining -= Time.deltaTime;

        }

        animator.SetBool("isGrounded", isGrounded);
    }

    // Runs every time you change something in the inspector of the component
    // or reset is called or when Unity recompiles, etc.
    private void OnValidate()
    {
        if (rb2d == null)
            rb2d = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (capsuleCollider == null)
            capsuleCollider = GetComponent<CapsuleCollider2D>();

    }


}
