using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class MergingDeez : MonoBehaviour
{
    public Vector3[] vertices;
    public int[] tris;

    public Material defaultMat;
    
    public MeshDataContainer[] closestVerticesTocentre = new MeshDataContainer[4];
    
    // public int[] thisMeshTris;
    // public Vector3[] thisMeshesVertices;
    
    public bool isMerging = false;
    public bool runCollisionOnThis = true;


    private MeshCollider globalCollider;
    private Vector3 globalNewCentre;
    
    public ContactPoint[] contacts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // thisMeshTris = GetComponent<MeshFilter>().mesh.triangles;
        // thisMeshesVertices = GetComponent<MeshFilter>().mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out MergingDeez d) && !isMerging)
        {
            if (!runCollisionOnThis) return;
            
            other.collider.GetComponent<MergingDeez>().runCollisionOnThis = false;
            Debug.LogWarning($"contact point is: {other.GetContact(0).point}");

            Vector3[] tempVert = other.collider.GetComponent<MeshFilter>().mesh.vertices;
            Transform otherMeshtrans = other.transform;

            Vector3 hitPoint = other.GetContact(0).point;
            
            //other.providesContacts.
            isMerging = true;
            Debug.LogWarning("merging in attempt");
            //call merge attempt function
            MergeCubes(tempVert, otherMeshtrans.position, hitPoint, 
                other.transform, other.collider.GetComponent<MeshFilter>().mesh.triangles);
            
            Destroy(other.gameObject);
            runCollisionOnThis = false;
        }
    }

 

    public void MergeCubes(Vector3[] otherMeshVerts, Vector3 otherMeshPosition, Vector3 newMeshCentre, Transform otherMeshTransform, int[] otherMeshTris)
    {
        MeshFilter thisFilter = GetComponent<MeshFilter>();

        globalNewCentre = newMeshCentre;
        Vector3[] ourVerts = thisFilter.mesh.vertices;
        
        Debug.LogWarning($"mesh 1 vertices length: {ourVerts.Length} and mesh 2 vertice length is {otherMeshVerts.Length}");
        
        vertices = new Vector3[otherMeshVerts.Length + ourVerts.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = -Vector3.one;
        }
        
        //this mesh's vertices
        AppendArray(vertices, ourVerts, calculateNewOffset(transform.position, newMeshCentre), transform);
       
        //other mesh vertices
        AppendArray(vertices, otherMeshVerts, calculateNewOffset(otherMeshPosition, newMeshCentre), otherMeshTransform);
        
        
        //creation of new mesh
        GameObject mergedMesh = Instantiate(new GameObject(), newMeshCentre, Quaternion.identity);

        mergedMesh.name = "merged";
        
        
        MeshFilter mf = mergedMesh.AddComponent<MeshFilter>();
        MeshRenderer mr = mergedMesh.AddComponent<MeshRenderer>();

        Mesh mish = new Mesh();

        mf.mesh = mish;
        mf.sharedMesh = mish;
        mish.vertices = vertices;
        mr.materials[0] = defaultMat;
        
        //uv generation
        Vector2[] uvs = new Vector2[vertices.Length];

        int numOfTristhisMesh = thisFilter.mesh.triangles.Length;
        int numOfTrisOtherMesh = otherMeshTris.Length;
        tris = new int[ numOfTristhisMesh + numOfTrisOtherMesh];
        
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        mish.uv = uvs;

        for (int i = 0; i < tris.Length; i++)
        {
            tris[i] = -1;
        }
        
        //this Mesh
        AppendArray(tris, thisFilter.mesh.triangles, 0);
        
        //external mesh
        AppendArray(tris, otherMeshTris, thisFilter.mesh.vertices.Length); // --> offsetting by to bring to the middle of the vertice array for the second mesh being element 23

        mish.triangles = tris;

        //colour
        Color mainColour = new Color(0.4f, 0.3f, 0.7f);

        Color[] colours = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            colours[i] = mainColour;
        }
        
        mish.colors = colours;

        closestVerticesTocentre = FindClosestPoints(vertices, mergedMesh.transform.position);
        
        
        
        // Recalculate bounds and normals
        mish.RecalculateBounds();
        mish.RecalculateNormals();

        MeshCollider collider = mergedMesh.AddComponent<MeshCollider>();
        globalCollider = collider;
        collider.convex = true;
        
        Vector3[] tempVerts2 = collider.sharedMesh.vertices;
        
        int[] tempTris2 = collider.sharedMesh.triangles; 
        
        // mish.Clear();
        
        
        
        
        Rigidbody rb = mergedMesh.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.linearDamping = 10f;

        MergingDeez md = mergedMesh.AddComponent<MergingDeez>();
        md.defaultMat = defaultMat;
        md.isMerging = false;

        md.vertices = vertices;
        md.tris = tris;
        md.closestVerticesTocentre = closestVerticesTocentre;
        md.defaultMat = defaultMat;

        StartCoroutine(nameof(somehting));

        
        
       

    }


    public MeshDataContainer[] FindClosestPoints(Vector3[] vertices, Vector3 originPos)
    {
        float closestDistance = 0f;
        Vector3 closestPoint = Vector3.positiveInfinity;

        MeshDataContainer[] closestPoints = new MeshDataContainer[4]
        {
            new MeshDataContainer(-1, Vector3.positiveInfinity),
            new MeshDataContainer(-1, Vector3.positiveInfinity),
            new MeshDataContainer(-1, Vector3.positiveInfinity),
            new MeshDataContainer(-1, Vector3.positiveInfinity)
        };

        for (int i = 0; i < vertices.Length; i++)
        {
            float tempDist = Vector3.Distance(originPos, vertices[i]);
            float lowestPoint = Vector3.Distance(originPos, closestPoints[3].point);
            
            if (tempDist < lowestPoint)
            {
                closestPoints[3].point = vertices[i]; //replace the lowest vertice with new one
                closestPoints[3].locationInVerticeA = i;
                
                //Array.Sort(closestPoints);
                Array.Sort(closestPoints, (a, b) =>
                    Vector3.Distance(originPos, a.point).CompareTo(Vector3.Distance(originPos, b.point)));
            }
        }

        return closestPoints;
    }

    public IEnumerator somehting()
    {

        yield return new WaitForEndOfFrame();
        //creation of new mesh
        GameObject mergedMesh1 = Instantiate(new GameObject(), globalNewCentre, Quaternion.identity);
        
        mergedMesh1.name = "merged2";
        
        
        MeshFilter mf1 = mergedMesh1.AddComponent<MeshFilter>();
        MeshRenderer mr1 = mergedMesh1.AddComponent<MeshRenderer>();
        
        Mesh mish1 = new Mesh();
        
        mf1.mesh = globalCollider.sharedMesh;
        mf1.sharedMesh = globalCollider.sharedMesh;
        mish1.vertices = globalCollider.sharedMesh.vertices;
        mish1.triangles = globalCollider.sharedMesh.triangles;
        mr1.materials[0] = defaultMat;
        
        //uv generation
        Vector2[] uvs1 = globalCollider.sharedMesh.uv;
        
        
        // Recalculate bounds and normals
        mish1.RecalculateBounds();
        mish1.RecalculateNormals();
        
        gameObject.SetActive(false);
    }
    
    /// <summary>
    /// merging arrays into one
    /// </summary>
    /// <param name="ArrayToAppend"></param>
    /// <param name="arrayTocombine"></param>
    public void AppendArray(Vector3[] ArrayToAppend, Vector3[] arrayTocombine, Vector3 newOffset, Transform transformReference)
    {
        int j = 0; //position in global vertice array
        int k = 0; //position of arrayToCombine
        
        
        //find startpoint
        while (ArrayToAppend[j] != -Vector3.one)
        {
            j++;
        }
        
        for (int i = j; k < arrayTocombine.Length; i++)
        {
            //Vector3 globalPoint = transformReference.TransformPoint(arrayTocombine[k]);
            
            ArrayToAppend[i] = arrayTocombine[k] + newOffset; //offsetting the vertice to match our new centre
             
            // ArrayToAppend[i].Normalize(); //normalizing the value to give curve
            
            k++;
        }
    }

    public void AppendArray(int[] globalTris, int[] trisToMerge, int offsetTriArray)
    {
        int j = 0; //position in global vertice array
        int k = 0; //position of arrayToCombine
        
        
        //find startpoint
        while (globalTris[j] != -1)
        {
            j++;
        }
        
        for (int i = j; k < trisToMerge.Length; i++)
        {
            //Vector3 globalPoint = transformReference.TransformPoint(arrayTocombine[k]);
            
            globalTris[i] = trisToMerge[k] + offsetTriArray; //offsetting the vertice to match our new centre
            
            k++;
        }
    }
    
    public Vector3 calculateNewOffset(Vector3 OldCentrePos, Vector3 NewCentrePos)
    {
        Vector3 diff = OldCentrePos - NewCentrePos;

        return diff; //return the difference to offset the vertices
    }
    
}
/// <summary>
/// 
/// </summary>
[Serializable]
public class MeshDataContainer
{

    public MeshDataContainer(int posInArray, Vector3 inputPoint)
    {
        locationInVerticeA = posInArray;
        point = inputPoint;
    }
    
    public int locationInVerticeA;
    public Vector3 point;
}
