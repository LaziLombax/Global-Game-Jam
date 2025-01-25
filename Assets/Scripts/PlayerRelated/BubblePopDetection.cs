using System;
using UnityEngine;

public class BubblePopDetection : MonoBehaviour
{
    public Shoot gun;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 3) //if on the bubble layer we do something
        {
            gun.RegenerateResource(other.gameObject.GetComponent<BubbleClusterManMa>().totalBubblesInCluster);
            
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
