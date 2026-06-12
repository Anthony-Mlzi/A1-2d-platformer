using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonLoadScene : MonoBehaviour
{
    // 000982050 Anthony Mallozzi GAME 10009 Assignment One

    // Load the second level on click

    public void LoadScene()
    {
        SceneManager.LoadScene(3);
    }
}
