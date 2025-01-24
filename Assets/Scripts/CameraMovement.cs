using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private void Start()
    {

    }
    

    private void Update()
    { 
        transform.Rotate(new Vector3(0.0f, Input.mousePositionDelta.x, 0.0f));

        Camera.main.transform.Rotate(-Input.mousePositionDelta.y, 0.0f, 0.0f);
        Debug.Log(Camera.main.transform.localRotation.eulerAngles.x);
        if (Camera.main.transform.localRotation.eulerAngles.x > 45.0f && Camera.main.transform.localRotation.eulerAngles.x < 315.0f)
        {
            if (Camera.main.transform.localRotation.eulerAngles.x < 180.0f)
            {
                Camera.main.transform.SetPositionAndRotation(transform.position, transform.rotation);
                Camera.main.transform.Rotate(44.0f, 0.0f, 0.0f);
            }
            else
            {
                Camera.main.transform.SetPositionAndRotation(transform.position, transform.rotation);
                Camera.main.transform.Rotate(-44.0f, 0.0f, 0.0f);
            }
        }

    }
}
