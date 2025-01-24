using UnityEditor;
using UnityEngine;

public class JackMerge : MonoBehaviour
{
    bool runCollisionOnThis = true;
    public GameObject collisionPoint;
    public GameObject sphere;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (runCollisionOnThis)
        {
            collision.gameObject.GetComponent<JackMerge>().runCollisionOnThis = false;
            collisionPoint.transform.position = collision.GetContact(0).point;
            GameObject vertex = Instantiate(sphere);
            vertex.transform.parent = transform;
            vertex.transform.position = Vector3.zero; 
            EditorApplication.isPaused = true;
            
            Transform destroyedSphere = collision.gameObject.GetComponentsInChildren<Transform>()[1];
            Debug.Log(vertex.transform.localPosition);
            Debug.Log(destroyedSphere.localPosition);
            Debug.Log(this.transform.position);
            Debug.Log(collisionPoint.transform.position);
            vertex.transform.localPosition = destroyedSphere.localPosition + (- this.transform.position + collisionPoint.transform.position);
            Debug.Log(vertex.transform.localPosition);
            collision.gameObject.SetActive(false);
        }
    }
}
