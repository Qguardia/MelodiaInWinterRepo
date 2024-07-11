using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static FPSMovement;

public class CoinCounter : MonoBehaviour
{
    // Start is called before the first frame update

    public static CoinCounter instance;

    public TMP_Text CoinText;
    public int currentCoins;

    void Awake()
    {
        instance = this;
        FPSMovement CoinsRemaining;
        CoinsRemaining = GetComponent<FPSMovement>();
       
    }
    void Start()
    {
        CoinText.text = "Coins remaining: " + currentCoins.ToString();
    }
    public void LoseCoins(int v)
    {
        if(currentCoins > 0)
        {
            currentCoins -= v;
            CoinText.text = "Coins remaining: " + currentCoins.ToString();
        }
    }
    public void OutofCoins()
    {
        CoinText.text = "Out of Coins";
    }
}