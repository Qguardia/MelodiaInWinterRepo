using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeafeningNoteProjectile : MonoBehaviour
{
    public float NoteSpeed;
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * NoteSpeed;
    }

    void OnTriggerEnter(Collider col)
    {
        NavmeshAgentScript navmeshComponent = col.GetComponent<NavmeshAgentScript>();
        NavMeshAgentSentry navmeshComponentSEN = col.GetComponent<NavMeshAgentSentry>();
        DestroySelf();

        if (navmeshComponent != null)
        {
            DestroySelf();
            navmeshComponentSEN.isStunned = true;
            navmeshComponent.AIState = 8; //Chase the player
        }
        if (navmeshComponentSEN != null)
        {
            DestroySelf();
            navmeshComponentSEN.isStunned = true;
            navmeshComponentSEN.AIState = 8;
        }
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

   /* void OnTriggerEnter(Collider col)
    {
        NavmeshAgentScript navmeshComponent = col.GetComponent<NavmeshAgentScript>();
        NavMeshAgentSentry navmeshComponentSEN = col.GetComponent<NavMeshAgentSentry>();

        if (navmeshComponent != null)
        {
            navmeshComponent.coinHeard = true;
            navmeshComponent.AIState = 8; //Chase the player
        }
        if (navmeshComponentSEN != null)
        {
            Debug.Log("Coin heard");
            navmeshComponentSEN.coinHeard = true;
            navmeshComponentSEN.AIState = 8;
        }
    }*/
}
