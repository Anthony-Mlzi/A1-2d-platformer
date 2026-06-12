using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{
    // 000982050 Anthony Mallozzi GAME 10009 Assignment One

    // Water collision
    public bool inWater = false;

    public void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.gameObject.CompareTag("Player"))
        {
            Debug.Log("Death");

            inWater = true;
            // Load retry scene on collision
            SceneManager.LoadScene(1);
        }
    }
}
