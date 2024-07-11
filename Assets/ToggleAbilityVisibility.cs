using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class ToggleAbilityVisibility : MonoBehaviour
{
    public GameObject AbilityToggle;

    FPSMovement canUseAbility_Music;

    // Start is called before the first frame update
    void Start()
    {
        canUseAbility_Music = GetComponent<FPSMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        /*while (canUseAbility_Music == true)
        {
            Debug.Log("cooldownvisible");
            AbilityToggle.SetActive(false);
            return;
        }
            AbilityToggle.SetActive(true);

        */
    }
}
