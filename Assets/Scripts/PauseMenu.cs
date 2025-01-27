using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Assign the pause menu UI panel in the Inspector
    private bool isPaused = false;

    public CameraMovement camMove;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        camMove.isDisabled = true;
        isPaused = true;
        Time.timeScale = 0; // Freeze the game
        pauseMenu.SetActive(true); // Show the pause menu
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        camMove.isDisabled = false;
        isPaused = false;
        Time.timeScale = 1; // Resume the game
        pauseMenu.SetActive(false); // Hide the pause menu
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1; // Ensure the game isn't frozen when returning to the main menu
        SceneManager.LoadScene("TitleScreen"); // Load the main menu scene
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the game
        Debug.Log("Game Quit"); // Debug log for testing in the editor
    }
}