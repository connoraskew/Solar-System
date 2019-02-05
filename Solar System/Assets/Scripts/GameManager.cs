using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// change rings to go slightly dull
// rotate camera in cinema mode
// have a single text in the centre to display both the planet and info text,
// due to display issues with two texts centred around difference center points

public class GameManager : MonoBehaviour
{
    public bool isCinematicMode;

    public GameObject background;
    public GameObject ringsHolder;
    public GameObject planetSelectorFadeCanvas;
    public GameObject infoSelectorFadeCanvas;

    public Sprite eyeClosed;
    public Sprite eyeOpen;

    public Button cinematicButton;

    public PlanetCollectiveInfo[] planetCollectiveInfo;

    public Text planetText;
    public Text infoText;

    public int planetIndex;
    public int infoIndex;

    void Awake()
    {
        isCinematicMode = false;

        background.GetComponent<FadeController>().isFading = true;

        CinemaSetUp();

        PlanetSelector(0);
        InfoSelector(0);
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

            planetSelectorFadeCanvas.GetComponent<CanvasGroupController>().isFading = true;

            infoSelectorFadeCanvas.GetComponent<CanvasGroupController>().isFading = true;
        }
        else
        {
            background.GetComponent<FadeController>().isFading = true;

            for (int i = 0; i < ringsHolder.transform.childCount; i++)
            {
                ringsHolder.transform.GetChild(i).GetComponent<FadeController>().isFading = false;
            }

            cinematicButton.image.sprite = eyeOpen;

            planetSelectorFadeCanvas.GetComponent<CanvasGroupController>().isFading = false;

            infoSelectorFadeCanvas.GetComponent<CanvasGroupController>().isFading = false;
        }
    }

    public void PlanetSelector(int index)
    {
        planetText.text = planetCollectiveInfo[index].name;

        planetIndex = index;

        InfoSelector(infoIndex);
    }

    public void InfoSelector(int index)
    {
        switch (index)
        {
            default:
                infoText.text = planetCollectiveInfo[planetIndex].Diameter;
                break;
            case 1:
                infoText.text = planetCollectiveInfo[planetIndex].DistanceFromSun;
                break;
            case 2:
                infoText.text = planetCollectiveInfo[planetIndex].OrbitDuration;
                break;
        }

        infoIndex = index;
    }
}

[System.Serializable]
public struct PlanetCollectiveInfo
{
    public string name;

    public string Diameter;

    public string DistanceFromSun;

    public string OrbitDuration;
}