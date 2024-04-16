using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalCoinScript : MonoBehaviour
{
    public float CoinSpeed;
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * CoinSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CoinHasRemoveditself");
        StartCoroutine(WaitTime());
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(10.0f);
        DestroySelf();
    }
}
