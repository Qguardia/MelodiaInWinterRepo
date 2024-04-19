using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUiGroup;

    // Fade in and fade out booleans
    [SerializeField] private bool fadein = false;
    [SerializeField] private bool fadeOut = false; 

    public void ShowUi()
    {
        myUiGroup.alpha = 1;
    }

    public void HideUI()
    {
        myUiGroup.alpha = 0;
    }

    private void Update()
    {
        if (fadein)
        {
            if (myUiGroup.alpha < 1)
            {
                myUiGroup.alpha += Time.deltaTime;
                if (myUiGroup.alpha >= 1)
                {
                    fadein = false;
                }
            }
        }
        if (fadeOut)
        {
            if (myUiGroup.alpha >= 0)
            {
                myUiGroup.alpha += Time.deltaTime;
                if (myUiGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }
}
