using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitBoundary : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if(other.CompareTag("Player")) ResetScene();
   }

   public void ResetScene()
   {
      Scene currentScene = SceneManager.GetActiveScene();
      SceneManager.LoadScene(currentScene.name);
   }
}
