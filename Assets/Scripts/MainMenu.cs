using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mapMenu; // Assign the map UI panel in the Inspector
    public GameObject settingsMenu; // Assign the settings UI panel in the Inspector
    
    public void PlayButton()
    {
        mapMenu.SetActive(true); // Show the map menu
        settingsMenu.SetActive(false); // Hide the settings menu if it's active
    }

    public void SettingsButton()
    {
        settingsMenu.SetActive(true); // Show the settings menu
        mapMenu.SetActive(false); // Hide the map menu if it's active
    }

    public void QuitButton()
    {
        Application.Quit(); // Quit the game
        Debug.Log("Game Quit"); // Debug log for testing in the editor
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex); // Load the selected level by index
    }

    public void BackToMainMenu()
    {
        mapMenu.SetActive(false); // Hide the map menu
        settingsMenu.SetActive(false); // Hide the settings menu
    }
}