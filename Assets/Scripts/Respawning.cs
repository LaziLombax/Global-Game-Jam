using UnityEngine;

public class Respawning : MonoBehaviour
{


    private static Respawning _instance;
    public static Respawning Instance
    {
        get
        {
            _instance = FindAnyObjectByType<Respawning>();
            return _instance;
        }
    }
    public Transform currentSpawnPoint;
    public Transform player;
    public void RespawnPlayer()
    {
        player.position = currentSpawnPoint.position;
        player.rotation = currentSpawnPoint.rotation;
    }
}
