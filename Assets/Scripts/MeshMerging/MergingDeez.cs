using System;
using UnityEngine;

public class MergingDeez : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MergingDeez d))
        {
            Debug.LogWarning("merging in attempt");
            //call merge attempt function
            MergeCubes();
        }
    }

    public void MergeCubes()
    {
        GameObject mergedMesh = Instantiate(new GameObject(), transform.position, Quaternion.identity);

        mergedMesh.name = "merged";
        
        //creation of new mesh
        MeshFilter mf = mergedMesh.AddComponent<MeshFilter>();
        MeshRenderer mr = mergedMesh.AddComponent<MeshRenderer>();

        Mesh mish = new Mesh();

        mf.mesh = mish;
        



    }
}
