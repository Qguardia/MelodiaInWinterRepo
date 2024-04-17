using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbility : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        FPSMovement navmeshComponent = col.GetComponent<FPSMovement>();

        if (navmeshComponent = null)
        {
            navmeshComponent.canUseAbility_Coin = false;
        }
    }
}
