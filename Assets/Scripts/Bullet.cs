using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 100.0f;
    public GameObject splatEffect;
    
    //Damage Scripts below
    public bool instaDeathAttacker;
    public float attackRate;
    public float damageMulitplier = 1.0f;
    private float timer;
    public GameObject player;
    public float health;

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
           // Instantiate(splatEffect, collision.transform.position, Quaternion.identity);
           // Destroy(collision.gameObject); //TEMP INSTAKILL

            
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
        private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (instaDeathAttacker == true)
            {
                other.gameObject.GetComponent<PlayerHealth>().playerDeath();
            }
            else
            {
                health = other.gameObject.GetComponent<PlayerHealth>().playerHealth;

                timer = timer + Time.deltaTime;

                if (timer >= attackRate)
                {
                    other.gameObject.GetComponent<PlayerHealth>().playerHealth = health - 1 * damageMulitplier;
                    timer = 0.0f;
                }
            }
            
        }
    }
}
