using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour
{
    private BoxCollider soundBox;
    private Vector3 triggerScaleBase;

    void Start()
    {
        soundBox = GetComponent<BoxCollider>();
        triggerScaleBase = soundBox.size;
    }

    void OnTriggerEnter(Collider col) 
    {
        NavmeshAgentScript navmeshComponent = col.GetComponent<NavmeshAgentScript>();
        
        if (navmeshComponent != null) 
        {
            navmeshComponent.AIState = 1; //Chase the player
        }
    }

    public void NormalSoundRange()
    {
        soundBox.size = triggerScaleBase;
    }

    public void CrouchSoundRange()
    {
        soundBox.size = new Vector3 (0.5f * triggerScaleBase.x, 0.5f * triggerScaleBase.y, 0.5f * triggerScaleBase.z);
    }
}
