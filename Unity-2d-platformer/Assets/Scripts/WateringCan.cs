using UnityEngine;
using UnityEngine.SceneManagement;

public class WateringCan : MonoBehaviour
{
    // 000982050 Anthony Mallozzi GAME 10009 Assignment One

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

            if (numberCollected >= 6)
            {
                LoadScene();
            }
        }
    }

    // Switch scenes if all collected
    public void LoadScene()
    {
        Debug.Log("6 collected");
        SceneManager.LoadScene(2);
    }
}
