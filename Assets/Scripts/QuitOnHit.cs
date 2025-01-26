using UnityEngine;

public class QuitOnHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Application.Quit();
        }
    }
}
