using UnityEngine;
using UnityEngine.SceneManagement;
public class NewMonoBehaviourScript : MonoBehaviour
{
    // 000982050 Anthony Mallozzi GAME 10009 Assignment One

    public KeyCode ResetKey = KeyCode.R;

    void Update()
    {
        // If R key is pressed this frame
        if (Input.GetKeyDown(ResetKey) == true)
        {
            // Get current scene
            Scene currentScene = SceneManager.GetActiveScene();
            //Reset scene
            SceneManager.LoadScene(currentScene.buildIndex);
        }

    }
}
