using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //singleton creation
    public static GameManager gm;

    public AudioData audioLib;

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
}
