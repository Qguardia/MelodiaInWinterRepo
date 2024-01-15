using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
   public GameObject bulletPrefab;
   public GameObject bulletSpawnPoint;

   private bool shootingState = false;

   //Start is called before the first frame update
   void Start()
   {
      bulletSpawnPoint = transform.GetChild(0).gameObject;
   }

   public void TryToFireGun()
   {
      if (shootingState == false) 
      {
         StartCoroutine(FireDelay());
      }
   }

   private IEnumerator FireDelay()
   {
      shootingState = true;
      GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
      yield return new WaitForSeconds(0.4f);
      //GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
      shootingState = false;
   }
}