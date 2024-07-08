using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPopupTextEnvironment : MonoBehaviour
{
    [TextArea(5, 10)]
    [SerializeField] private string textToDisplay;

    private static TMP_Text dialoguebox;
    private static Image Background;

    void Start()
    {
        if (dialoguebox == null)
        {
            dialoguebox = GameObject.Find("DialogBox(Commentary)").GetComponent<TMP_Text>();
            Background = GameObject.Find("Background").GetComponent<Image>();
            dialoguebox.gameObject.SetActive(false);
            Background.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialoguebox.text = textToDisplay;
            dialoguebox.gameObject.SetActive(true);
            Background.gameObject.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialoguebox.gameObject.SetActive(false);
            Background.gameObject.SetActive(false);

        }
    }
}