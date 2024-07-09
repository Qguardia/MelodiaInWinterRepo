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
    public GameObject ProjectileSpawn;

    FPSMovement PlayerInput;

    public ViolinMode mode;

    void Start()
    {
        ProjectileSpawn = transform.GetChild(0).gameObject;
        PlayerInput = GetComponent<FPSMovement>();
    }

    void Update()
    {
        
    }

    public void Projectile()
    {
        GameObject Deafen_projectile = Instantiate(DeafeningProjectile, ProjectileSpawn.transform.position, ProjectileSpawn.transform.rotation);
    }

    public void Distraction()
    {
        //...
    }

}
