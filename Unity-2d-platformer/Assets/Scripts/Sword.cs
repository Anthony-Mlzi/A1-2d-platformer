using UnityEngine;
using UnityEngine.SceneManagement;

public class Sword : MonoBehaviour
{
    // 000982050 Anthony Mallozzi GAME 10009 Assignment One

    // For sword collection

    public bool swordCollected = false;

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        
        if (collider2d.gameObject.CompareTag("Player") == true)
        {
            // Log collection
            Debug.Log("Sword Collected");

            this.gameObject.SetActive(false);

            // Load win scene
            SceneManager.LoadScene(4);
        }

    }

}
