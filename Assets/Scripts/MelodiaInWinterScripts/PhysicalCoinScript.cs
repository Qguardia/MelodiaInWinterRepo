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
        DestroySelf();
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
