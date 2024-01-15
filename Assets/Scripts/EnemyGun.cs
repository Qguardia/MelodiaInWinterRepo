using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    GameObject bulletSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpawnPoint = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        /*if
        {
            GameObject bullet = Instantiate(bulletPrefab,bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        }*/
    }
}
