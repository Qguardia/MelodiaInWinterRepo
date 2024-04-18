using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDistraction : MonoBehaviour
{
    public BoxCollider CoinSoundBox;
    private Vector3 triggerScaleBase;
  

    void Start()
    {
        CoinSoundBox = GetComponent<BoxCollider>();
        triggerScaleBase = CoinSoundBox.size;
        CoinSoundBox.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        NavmeshAgentScript navmeshComponent = col.GetComponent<NavmeshAgentScript>();
        NavMeshAgentSentry navmeshComponentSEN = col.GetComponent<NavMeshAgentSentry>();
        PhysicalCoinScript Impact = gameObject.transform.parent.GetComponent<PhysicalCoinScript>();

        if (Impact.GroundHit == true)
        {
            Debug.Log("NoiseActive");
            CoinSoundBox.gameObject.GetComponent<BoxCollider>().enabled = true;
            if (navmeshComponent != null)
            {
                navmeshComponent.coinHeard = true;
                navmeshComponent.AIState = 7; //Chase the player
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
    // Update is called once per frame
    /*
    void OnTriggerEnter(Collider col)
    {
        NavmeshAgentScript navmeshComponent = col.GetComponent<NavmeshAgentScript>();
        NavMeshAgentSentry navmeshComponentSEN = col.GetComponent<NavMeshAgentSentry>();
        PhysicalCoinScript Impact = col.gameObject.GetComponent<PhysicalCoinScript>();

        

        
            if (Impact.GroundHit == true) 
            {
                Debug.Log("NoiseActive");
                CoinSoundBox.gameObject.GetComponent<BoxCollider>().enabled = true;
                if (navmeshComponent != null)
                {
                    navmeshComponent.coinHeard = true;
                    navmeshComponent.AIState = 7; //Chase the player
                }
                if (navmeshComponentSEN != null)
                {
                    Debug.Log("Coin heard");
                    navmeshComponentSEN.coinHeard = true;
                    navmeshComponentSEN.AIState = 7;
                    // navmeshComponentSEN.coinHeard = true;
                }
            }
            //CoinSoundBox.gameObject.GetComponent<BoxCollider>().enabled = false;
        
    }*/
}

//}