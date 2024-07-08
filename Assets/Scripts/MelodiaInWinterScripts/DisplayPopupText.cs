using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayPopupText : MonoBehaviour
{
    [TextArea(5, 10)]
    [SerializeField] private string textToDisplay;

    private static TMP_Text dialoguebox;

    void Start()
    {
        if (dialoguebox == null)
        {
            dialoguebox = GameObject.Find("DialogBox").GetComponent<TMP_Text>();
            dialoguebox.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            dialoguebox.text = textToDisplay;
            dialoguebox.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialoguebox.gameObject.SetActive(false);
        }
    }
}
