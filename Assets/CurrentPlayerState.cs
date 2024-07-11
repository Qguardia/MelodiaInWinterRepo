using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static FPSMovement;

public class CurrentPlayerState : MonoBehaviour
{
    // Start is called before the first frame update

    public static CurrentPlayerState instance;

    public TMP_Text StatusText;
    public string currentState;

    void Awake()
    {
        instance = this;
        FPSMovement PlayerState;
        PlayerState = GetComponent<FPSMovement>();

    }
    void Start()
    {
        StatusText.text = currentState;
    }
  //  public void StateChange()
  //  {
  //      StatusText.text = currentState;
  //  }
    void Update()
    {
        StatusText.text = currentState;
    }
}