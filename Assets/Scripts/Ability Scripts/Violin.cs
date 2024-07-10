using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//'Gun' Script
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
        GameObject DeafeningNoteProjectile = Instantiate(DeafeningProjectile, ProjectileSpawn.transform.position, ProjectileSpawn.transform.rotation);
    }

    public void Distraction()
    {
            PlayerInput.soundBox.gameObject.SetActive(true);
            PlayerInput.soundBox.AbilitySoundRange();
            return;
    }

}
