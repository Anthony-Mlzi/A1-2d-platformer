using UnityEngine;

public class WateringCan : MonoBehaviour
{
    // Belongs ot class
    public static int numberCollected = 0;

    // Belongs to instance
    public bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        // See if collider is tagged as Player
        if (collider2d.gameObject.CompareTag("Player") == true)
        {

            // Increment number of these collected
            numberCollected += 1;
            isCollected = true;
            Debug.Log($"watering can collected: {numberCollected}");
            // Disable object on collection
            this.gameObject.SetActive(false);
            // This must be the last thing we do
        }
    }
}
