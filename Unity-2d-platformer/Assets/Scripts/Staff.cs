using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Staff : MonoBehaviour
{
    // 000982050 Anthony Mallozzi GAME 10009 Assignment One

    // staff collection bool
    public bool staffCollected = false;

    public Player player;

    public void OnTriggerEnter2D(Collider2D collider2d)
    {
        // See if collider is tagged as Player
        if (collider2d.gameObject.CompareTag("Player") == true)
        {
            // Set bool true
            staffCollected = true;

            // Increase max jump height
            player.maxJumpTime = 0.7f;

            // Destroy object
            this.gameObject.SetActive(false);
        }
    }
}
