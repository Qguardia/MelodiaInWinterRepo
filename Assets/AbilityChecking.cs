using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static FPSMovement;

public class AbilityChecking : MonoBehaviour
{
    // Start is called before the first frame update

    public static AbilityChecking instance;

    public TMP_Text AbilityText;
    public string currentAbility;

    void Awake()
    {
        instance = this;
        FPSMovement SelectedAbility;
        SelectedAbility = GetComponent<FPSMovement>();

    }
    void Start()
    {
        AbilityText.text = "Currently Selected" + currentAbility;
    }
    //  public void StateChange()
    //  {
    //      StatusText.text = currentState;
    //  }
    void Update()
    {
        AbilityText.text = "Currently selected: " + currentAbility;
    }

}
