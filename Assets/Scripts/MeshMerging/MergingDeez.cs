using System;
using System.Linq;
using UnityEngine;

public class MergingDeez : MonoBehaviour
{
    public Vector3[] vertices;

    private bool isMerging = false;

    public ContactPoint[] contacts;
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
        if (other.collider.TryGetComponent(out MergingDeez d) && !isMerging)
        {


            Debug.LogWarning($"contact point is: {other.GetContact(0).point}");
            
            //other.providesContacts.
            isMerging = true;
            Debug.LogWarning("merging in attempt");
            //call merge attempt function
            MergeCubes(other.collider.GetComponent<MeshFilter>().mesh.vertices.Distinct().ToArray(), other.collider.transform.position,
                other.GetContact(0).point);
        }
    }

 

    public void MergeCubes(Vector3[] otherMeshVerts, Vector3 otherMeshPosition, Vector3 newMeshCentre)
    {
        Vector3[] ourVerts = GetComponent<MeshFilter>().mesh.vertices.Distinct().ToArray();
        
        Debug.LogWarning($"mesh 1 vertices length: {ourVerts.Length} and mesh 2 vertice length is {otherMeshVerts.Length}");
        
        vertices = new Vector3[otherMeshVerts.Length + ourVerts.Length];
        
        //this mesh
        AppendVerticeArray(vertices, ourVerts, calculateNewOffset(transform.position, newMeshCentre));
       
        //other mesh
        AppendVerticeArray(vertices, otherMeshVerts, otherMeshPosition);
        
        GameObject mergedMesh = Instantiate(new GameObject(), newMeshCentre, Quaternion.identity);

        mergedMesh.name = "merged";
        
        //creation of new mesh
        MeshFilter mf = mergedMesh.AddComponent<MeshFilter>();
        MeshRenderer mr = mergedMesh.AddComponent<MeshRenderer>();

        Mesh mish = new Mesh();
        
        mf.mesh = mish;
        mf.sharedMesh = mish;
        
        
        


    }

    
    /// <summary>
    /// merging arrays into one
    /// </summary>
    /// <param name="ArrayToAppend"></param>
    /// <param name="arrayTocombine"></param>
    public void AppendVerticeArray(Vector3[] ArrayToAppend, Vector3[] arrayTocombine, Vector3 newOffset)
    {
        int j = 0;
        int k = 0;
        //find startpoint
        while (ArrayToAppend[j] != Vector3.zero)
        {
            j++;
        }
        
        for (int i = j; k < arrayTocombine.Length; i++)
        {
            ArrayToAppend[i] = arrayTocombine[k] + newOffset; //offsetting the vertice to match our new centre
            k++;
        }
    }

    public Vector3 calculateNewOffset(Vector3 OldCentrePos, Vector3 NewCentrePos)
    {
        Vector3 diff = OldCentrePos - NewCentrePos;

        return diff; //return the difference to offset the vertices
    }
    
}
