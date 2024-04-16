using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDistraction : MonoBehaviour
{
    private BoxCollider CoinSoundBox;
    private Vector3 triggerScaleBase;
    void Start()
    {
        CoinSoundBox = GetComponent<BoxCollider>();
        triggerScaleBase = CoinSoundBox.size;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        NavmeshAgentScript navmeshComponent = col.GetComponent<NavmeshAgentScript>();
        NavMeshAgentSentry navmeshComponentSEN = col.GetComponent<NavMeshAgentSentry>();
        
        if (navmeshComponent != null)
        {
            navmeshComponent.AIState = 1; //Chase the player
        }
        if (navmeshComponentSEN != null)
        {
            Debug.Log("Coin heard");
            navmeshComponentSEN.coinHeard = true;
            navmeshComponentSEN.AIState = 7;
           // navmeshComponentSEN.coinHeard = true;
        }
    }

}