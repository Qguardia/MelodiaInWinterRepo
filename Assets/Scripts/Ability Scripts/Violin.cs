using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Violin : MonoBehaviour
{
    public enum ViolinMode
    {
        Projectile,
        Distraction,
    }
    
    public GameObject DeafeningProjectile;
    GameObject ProjectileSpawn;

    FPSMovement PlayerInput;

    private ViolinMode mode;

    void Start()
    {
        ProjectileSpawn = transform.GetChild(0).gameObject;
        PlayerInput = GetComponent<FPSMovement>();
    }

    void Update()
    {
        /*
        int hello = 2;

        switch(hello)
        {
            case 1:
                Debug.Log("It is 1!");
                break;

            case 2:
                Debug.Log("It is 2!");
                break;

            case 3:
                Debug.Log("It is 3!");
                break;

            default:
                Debug.LogError("Invalid input for switch case!");
                break;
        }
        */

        switch (mode)
        {
            case ViolinMode.Projectile:
                if (Input.GetKeyDown(PlayerInput.m_MelodyAbility))
                {
                    GameObject DeafeningNoteProjectile = Instantiate(DeafeningProjectile, ProjectileSpawn.transform.position, ProjectileSpawn.transform.rotation);
                }
                break;
            case ViolinMode.Distraction:

                if (Input.GetKeyDown(PlayerInput.m_MelodyAbility))
                {
                    GameObject DeafeningNoteProjectile = Instantiate(DeafeningProjectile, ProjectileSpawn.transform.position, ProjectileSpawn.transform.rotation);
                }
                break;
        }
    }
}
