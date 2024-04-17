using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
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
       // Inspector.CapsuleCollider.SetActive(false); // trying to make the coin trigger the collider on impact
        Debug.Log("CoinHasRemoveditself");
        //StartCoroutine(WaitTime());

        DestroySelf();
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0f);
        DestroySelf();
    }
}
