using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera myCam;

    public LayerMask clickable;
    public LayerMask ground; 
 
    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            

            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.Log("shoot me");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                //hit clickable
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.ShiftclickSelect(hit.collider.gameObject);

                }
                else
                {
                    UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }
    }
}
