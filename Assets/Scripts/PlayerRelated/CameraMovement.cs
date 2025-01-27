using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool isDisabled = false;

    private void Awake()
    {
        
    }

    private void Start()
    {

    }
    

    private void Update()
    {
        if (isDisabled) return;
        
        transform.Rotate(new Vector3(0.0f, Input.mousePositionDelta.x, 0.0f));
        Camera.main.transform.Rotate(-Input.mousePositionDelta.y, 0.0f, 0.0f);

        // Below fixes bug where screen can go upside-down, still occurs at start or game somehow though
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
