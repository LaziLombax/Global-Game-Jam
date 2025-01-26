using UnityEngine;

public class SetSpawn : MonoBehaviour
{
    public Transform spawnTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Respawning.Instance.currentSpawnPoint = spawnTransform;
        }
    }
}
