using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour
{
    public BoxCollider soundBox;
    private Vector3 triggerScaleBase;

    void Start()
    {
        soundBox = GetComponent<BoxCollider>();
        triggerScaleBase = soundBox.size;
    }

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
            navmeshComponentSEN.AIState = 1;
        }
    }

    public void NormalSoundRange()
    {
        soundBox.size = triggerScaleBase;
    }

    public void CrouchSoundRange()
    {
        soundBox.size = new Vector3(0.5f * triggerScaleBase.x, 0.5f * triggerScaleBase.y, 0.5f * triggerScaleBase.z);
    }

    public void RunSoundRange()
    {
        soundBox.size = new Vector3(5f * triggerScaleBase.x, 5f * triggerScaleBase.y, 5f * triggerScaleBase.z);
    }

    public void AbilitySoundRange()
    {
        soundBox.size = new Vector3(10f * triggerScaleBase.x, 10f * triggerScaleBase.y, 10f * triggerScaleBase.z);
    }
}

