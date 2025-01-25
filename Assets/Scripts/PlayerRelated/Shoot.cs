using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bubbleBullet;
    [SerializeField] GameObject bubblePopperBullet;
    [SerializeField] Transform gunTip;
    [SerializeField] float minBubbleSize;
    [SerializeField] float maxBubbleSize;
    [SerializeField] float bubbleBlowupTime;
    [SerializeField] float shotSpeed;

    private float bubbleResource = 20f;
    private float resourceMultiplier = 2; //multiplies how much resource to take away
    
    float timeHeld = 0.0f;
    GameObject heldBubble;
    private GameObject bubblePopper;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && bubbleResource > 0){
            heldBubble = Instantiate(bubbleBullet, gunTip.position, gunTip.localRotation);
            // GameManager.gm.audioLib.
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

        if (Input.GetMouseButtonUp(1))
        {
            bubblePopper = Instantiate(bubblePopperBullet, gunTip.position, bubblePopperBullet.transform.rotation);
            bubblePopper.GetComponent<BubblePopDetection>().gun = this;
            bubblePopper.GetComponent<Rigidbody>().AddForce(gunTip.transform.up * shotSpeed, ForceMode.Impulse);
        }

    }

    void ReleaseBullet()
    {
        heldBubble.GetComponent<Rigidbody>().isKinematic = false;
        heldBubble.GetComponent<Rigidbody>().AddForce(gunTip.transform.up * shotSpeed, ForceMode.Impulse);
        TakeResourceAway(1f);
        heldBubble = null;
        timeHeld = 0.0f;   
    }

    public void TakeResourceAway(float resource)
    {
        bubbleResource -= (resource * resourceMultiplier);

        if (bubbleResource <= 0f) bubbleResource = 0f;
        
        Debug.LogWarning($"the new resource amount is: {bubbleResource}");
    }

    public void RegenerateResource(int clusterSize)
    {
        bubbleResource += clusterSize * resourceMultiplier;

        if (bubbleResource >= 20) bubbleResource = 20f;
        
        Debug.LogWarning($"the new resource amount is: {bubbleResource}");
    }
    
}
