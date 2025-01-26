using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    public string NextLevelToLoad;
    
    //when player enters the "win con" it loads the next level
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) GoToNextLevel(NextLevelToLoad);
    }

    public void GoToNextLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
