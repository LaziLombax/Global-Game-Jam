using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

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
        StartCoroutine(MusicHandler());
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
    
    IEnumerator MusicHandler ()
    {
        
        while (true)
        {
            string[] musicClips = new string[] { "floatinggarden", "hearty", "longnight","yesterday" };

            string selectedMusicClip = musicClips[Random.Range(0,3)];
            
            AudioSource musicSource = audioLib.AddNewAudioSourceFromStandard("GameManager", gameObject, selectedMusicClip);
            musicSource.Play();
            
            yield return new WaitForSeconds(225);
        }
    }
}
