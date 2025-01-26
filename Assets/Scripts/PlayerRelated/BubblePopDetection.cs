using System;
using UnityEngine;

public class BubblePopDetection : MonoBehaviour
{
    public Shoot gun;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,5f);
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
