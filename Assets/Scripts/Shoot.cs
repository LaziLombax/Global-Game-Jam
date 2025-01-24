using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bubbleBullet;
    [SerializeField] Transform gunTip;
    [SerializeField] float minBubbleSize;
    [SerializeField] float maxBubbleSize;
    [SerializeField] float bubbleBlowupTime;
    [SerializeField] float shotSpeed;

    float timeHeld = 0.0f;
    GameObject heldBubble;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            heldBubble = Instantiate(bubbleBullet, gunTip.position, gunTip.localRotation);
        }

        if (Input.GetMouseButton(0) && heldBubble) {
            timeHeld += Time.deltaTime;
            if (timeHeld > bubbleBlowupTime)
            {
                ReleaseBullet();
                return;
            }
            float scale = Mathf.Lerp(minBubbleSize, maxBubbleSize, timeHeld / bubbleBlowupTime);
            heldBubble.transform.localScale = new Vector3(scale, scale, scale);
            heldBubble.transform.position = gunTip.position;
        }

        if (Input.GetMouseButtonUp(0) && heldBubble)
        {
            ReleaseBullet();
        }

    }

    void ReleaseBullet()
    {
        heldBubble.GetComponent<Rigidbody>().isKinematic = false;
        heldBubble.GetComponent<Rigidbody>().AddForce(gunTip.transform.up * shotSpeed, ForceMode.Impulse);
        heldBubble = null;
        timeHeld = 0.0f;   
    }

}
