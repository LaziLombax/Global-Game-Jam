using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Assign the pause menu UI panel in the Inspector
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // Freeze the game
        pauseMenu.SetActive(true); // Show the pause menu
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // Resume the game
        pauseMenu.SetActive(false); // Hide the pause menu
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1; // Ensure the game isn't frozen when returning to the main menu
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game
        Debug.Log("Game Quit"); // Debug log for testing in the editor
    }
}