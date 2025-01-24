using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xpos = transform.position.x + Input.mousePosition.x;
        float ypos = transform.position.y + Input.mousePosition.y;
    }
}
