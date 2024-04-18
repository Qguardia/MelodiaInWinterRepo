using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PhysicalCoinScript : MonoBehaviour
{
    public bool GroundHit;
    public float CoinSpeed;
  //  public BoxCollider soundbox;
    private Vector3 triggerScaleBase;

    private BoxCollider cointrigger;


    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * CoinSpeed;
        //     soundbox = gameObject.GetComponent<BoxCollider>();
        //   soundbox.gameObject.GetComponent<BoxCollider>().enabled = false;
        /*  
          soundbox = GetComponent<BoxCollider>();
          triggerScaleBase = soundbox.size;
          soundbox.gameObject.GetComponent<BoxCollider>().enabled = false;*/
        cointrigger = GetComponentInChildren<BoxCollider>();
    }

    private void OnCollisionEnter(Collision Default)
    {
        // Inspector.CapsuleCollider.SetActive(false); // trying to make the coin trigger the collider on impact
        GroundHit = true;
        cointrigger.enabled = true;
        Debug.Log("Coin has Hit Ground");
        StartCoroutine(WaitTime());

       // StartCoroutine(WaitTime());
        // soundbox.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
    private IEnumerator WaitTime()
    {
        
        yield return new WaitForSeconds(0.1f);
        GroundHit = false;

        DestroySelf();
    }

   /* public void CoinSoundRange()
    {
        soundbox.size = triggerScaleBase;
    }*/
}
