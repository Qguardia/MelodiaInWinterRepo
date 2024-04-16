using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed /*= 100f*/;
    public GameObject splatEffect;
    
    //Damage Scripts below
    public bool instaDeathAttacker;
    public float attackRate;
    public float damageMulitplier;
    private float timer;
    public GameObject player;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
        Invoke("DestroySelf", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            currentHealth = collision.gameObject.GetComponent<PlayerHealth>().playerHealth;

            // Instantiate(splatEffect, collision.transform.position, Quaternion.identity);
            // Destroy(collision.gameObject); //TEMP INSTAKILL
            collision.gameObject.GetComponent<PlayerHealth>().playerHealth = currentHealth - (75);
            
           DestroySelf();
        }
        if (collision.gameObject.tag != "Enemy")
        {
            DestroySelf();
        }
 
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
