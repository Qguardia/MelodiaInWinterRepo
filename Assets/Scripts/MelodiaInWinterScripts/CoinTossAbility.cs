using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTossAbility : MonoBehaviour
{
    public GameObject CoinModel;
    GameObject CoinThrowPosition;
    void Start()
    {
        CoinThrowPosition = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame

    public void CoinThrow()
    {
        Debug.Log("coinIsthrown");
        GameObject Coin = Instantiate(CoinModel, CoinThrowPosition.transform.position, CoinThrowPosition.transform.rotation);
    }
}
