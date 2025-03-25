using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
   public void playGame ()
    {
        Debug.Log("Play button pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Make sure to update this to say +1 whenever a level is finished!!!!!!
    }

    public void exitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
