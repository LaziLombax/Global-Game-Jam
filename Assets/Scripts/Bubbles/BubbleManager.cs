using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BubbleManager : MonoBehaviour
{
    public Dictionary<Vector3, GameObject> bubbleDictionary;
    public GameObject bubblePrefab;

    public void AddBubble(IBubble bubble, Vector3 pos)
    {
        //Spawn Bubble
        GameObject bubbleTemp = Instantiate(bubblePrefab, pos, bubblePrefab.transform.rotation);
        bubbleTemp.AddComponent<Bubble>();
        
        Bubble BT = bubbleTemp.GetComponent<Bubble>();
        BT.Radius = bubble.Radius;
        BT.Flotability = bubble.Flotability;
        BT.Lifetime = bubble.Lifetime;
        BT.Type = bubble.Type;
        
        //scale bubble
        bubbleTemp.GetComponent<SphereCollider>().radius = BT.Radius;

        //Add to Dictionary
        bubbleDictionary[pos] = bubbleTemp;
    }

    public void RemoveBubble(Vector3 pos)
    {
        GameObject bubbleTemp = bubbleDictionary[pos];
        bubbleDictionary.Remove(pos);
        Destroy(bubbleTemp); //placeholder
    }

    public Bubble GetClosestBubble(Vector3 origin)
    {
        float minDist = Int32.MaxValue;
        Bubble closestBubble = null;
        
        foreach(var pair in bubbleDictionary)
        {
            Vector3 bubblePosition = pair.Key;
            float dist = Mathf.Abs(Vector3.Distance(origin, bubblePosition));
            if (dist < minDist)
            {
                minDist = dist;
                closestBubble = pair.Value.GetComponent<Bubble>();
            }
        }

        return closestBubble;
    }
}
