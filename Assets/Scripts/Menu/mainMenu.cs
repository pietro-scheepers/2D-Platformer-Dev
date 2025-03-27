using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject mapPanel;
    public GameObject[] mapPieces;
    // Simulate player's progress - GET FROM SAVE FILE!!!!!!!
    public int levelsCompleted = 0;
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

    public void OpenMap()
    {
        mainMenuPanel.SetActive(false);
        mapPanel.SetActive(true);
        UpdateMap();
    }

    public void CloseMap()
    {
        mapPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        mapPieces[levelsCompleted].SetActive(false);
    }

    void UpdateMap()
    {
        Debug.Log("Levels completed: " + levelsCompleted);
        mapPieces[levelsCompleted].SetActive(true);
    }
}
