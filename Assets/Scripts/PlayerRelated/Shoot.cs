using System;
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
    
    string selectedMusicClipBlow = "BubbleCharge";
    string selectedMusicClipGun = "BubbleLaunch";

    private AudioSource asb;
    private AudioSource asg;
    
    /// <summary>
    /// string selectedMusicClip = musicClips[Random.Range(0,3)];
            
//    AudioSource musicSource = audioLib.AddNewAudioSourceFromStandard("GameManager", gameObject, selectedMusicClip);
  //  musicSource.Play();

  /// </summary>
  ///
  ///
  private void Start()
  {
      asb = GameManager.gm.audioLib.AddNewAudioSourceFromStandard("Player", gameObject, selectedMusicClipBlow);
      asg = GameManager.gm.audioLib.AddNewAudioSourceFromStandard("Player", gameObject, selectedMusicClipGun);
      
      //= musicClips[Random.Range(0,3)]
  }

  private void Update()
    {
        if (Input.GetMouseButtonDown(0) && bubbleResource > 0){
            asg.Stop();
            heldBubble = Instantiate(bubbleBullet, gunTip.position, gunTip.localRotation);
            asb.Play();
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
            asb.Stop();
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
        asg.Play();
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
