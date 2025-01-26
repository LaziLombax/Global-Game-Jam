using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //singleton creation
    public static GameManager gm;

    public AudioData audioLib;

    public Transform currentSpawnPoint;
    public Transform player;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            _instance = FindAnyObjectByType<GameManager>();
            return _instance;
        }
    }
    private void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(this);
        }
        else
        {
            gm = this;
        }
        
        DontDestroyOnLoad(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        player.position = currentSpawnPoint.position;
        player.rotation = currentSpawnPoint.rotation;
    }
}
