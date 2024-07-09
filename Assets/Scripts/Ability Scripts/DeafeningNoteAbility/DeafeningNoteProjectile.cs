using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//'Bullet' SCript
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
   

        if (navmeshComponent != null)
        {
            navmeshComponent.AIState = 8;
            Debug.Log("Is Hit");
            navmeshComponent.isStunned = true;

            DestroySelf();
           // navmeshComponentSEN.isStunned = true;
        }
        if (navmeshComponentSEN != null)
        {
            //navmeshComponentSEN.isStunned = true;
            navmeshComponentSEN.AIState = 8;
            navmeshComponent.isStunned = true;
            DestroySelf();
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
