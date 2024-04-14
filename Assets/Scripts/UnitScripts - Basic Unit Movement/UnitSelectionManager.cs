using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; set; }

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> UnitsSelected = new List<GameObject>();

    public Camera cam;
    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;

    private void Awake()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    clickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift) == false)
                {
                    DeselectAll();
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && UnitsSelected.Count > 0)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;

                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }
        }
    }
     
    private void clickSelect(GameObject unit)
    {
        DeselectAll();
        UnitsSelected.Add(unit);
        UnitSelectedIndicator(unit, true);
        EnableUnitMovement(unit, true);
    }
    private void EnableUnitMovement(GameObject unit, bool shouldMove)
    {
        unit.GetComponent<UnitMovement>().enabled = shouldMove; 
    }
    private void UnitSelectedIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }
    private void DeselectAll()
    {
        foreach (var unit in UnitsSelected)
        {
            EnableUnitMovement(unit, false);
            UnitSelectedIndicator(unit, false);
        }
        groundMarker.SetActive(false);
        UnitsSelected.Clear();
    }
    private void MultiSelect(GameObject Unit)
    {
        if (UnitsSelected.Contains(Unit) == false)
        {
            UnitsSelected.Add(Unit);
            UnitSelectedIndicator(Unit, true);
            EnableUnitMovement(Unit, true);
        }
        else
        {
            EnableUnitMovement(Unit, false);
            UnitSelectedIndicator(Unit, false);
            UnitsSelected.Remove(Unit);
        }
    }
}
