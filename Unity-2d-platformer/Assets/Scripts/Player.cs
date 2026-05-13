using UnityEngine;

public class Player : MonoBehaviour
{
    // VARIABLES
    // We want to know about the player's Rigidbody2D component to add forces to it
    public Rigidbody2D rb2d;
    // We want the player's animator component to synchronize its staes to player movement
    public Animator animator;
    // How fast do we want the player to move
    public float speedX = 1f;

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
        // Get the player's movement input from Unity's legacy input system
        float moveX = Input.GetAxis("Horizontal");
        // Math.Abs() gives is the number's absolute value
        // Abs(+1) and Abs(-1) both give us +1
        if (Mathf.Abs(moveX) > 0.1f)
        {
            //Calculate the force to apply to the player (in Newtons if you're privy to that)
            float force = moveX * speedX;
            rb2d.AddForceX(force, ForceMode2D.Force);
        }
        // Sync the animator's parameters to the player's movement so it may
        // automatically control the player's animation
        animator.SetFloat("moveSpeedX", Mathf.Abs(moveX));
    }
}
