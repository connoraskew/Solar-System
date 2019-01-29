using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public bool isCinematicMode;

    public GameObject background;
    public GameObject ringsHolder;

    public Sprite eyeClosed;
    public Sprite eyeOpen;

    public Button cinematicButton;

    void Awake()
    {
        isCinematicMode = false;

        background.GetComponent<FadeController>().isFading = true;

        CinemaSetUp();
    }

    public void CinematicButtonPress()
    {
        isCinematicMode = !isCinematicMode;

        CinemaSetUp();
    }

    void CinemaSetUp()
    {
        if (isCinematicMode)
        {
            background.GetComponent<FadeController>().isFading = false;

            for (int i = 0; i < ringsHolder.transform.childCount; i++)
            {
                ringsHolder.transform.GetChild(i).GetComponent<FadeController>().isFading = false;
            }

            cinematicButton.image.sprite = eyeClosed;
        }
        else
        {
            background.GetComponent<FadeController>().isFading = true;

            for (int i = 0; i < ringsHolder.transform.childCount; i++)
            {
                ringsHolder.transform.GetChild(i).GetComponent<FadeController>().isFading = false;
            }

            cinematicButton.image.sprite = eyeOpen;
        }
    }
}