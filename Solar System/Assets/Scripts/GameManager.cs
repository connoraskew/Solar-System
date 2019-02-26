using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// make a quiz
// add an info button which explains how to use and what each of the buttons do

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isCinematicMode;

    [SerializeField] private GameObject planetContainer;

    [SerializeField] private GameObject ringsHolder;

    [SerializeField] private GameObject planetSelectorFadeCanvas;

    [SerializeField] private GameObject infoSelectorFadeCanvas;

    [SerializeField] private GameObject mainTextFadeCanvas;

    [SerializeField] private GameObject UIRingsHolder;

    [SerializeField] private CameraController mainCamera;

    [SerializeField] private Sprite eyeClosed;
    [SerializeField] private Sprite eyeOpen;

    [SerializeField] private Button cinematicButton;

    [SerializeField] private PlanetCollectiveInfo[] planetCollectiveInfo;

    [SerializeField] private Text PlanetInfoText;

    [SerializeField] private int planetIndex;
    [SerializeField] private int infoIndex;

    [SerializeField] private LayerMask ringsLayer;
    [SerializeField] private LayerMask planetsLayer;

    void Awake()
    {
        isCinematicMode = false;

        CinemaSetUp();

        PlanetSelector(0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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
            for (int i = 0; i < ringsHolder.transform.childCount; i++)
            {
                ringsHolder.transform.GetChild(i).GetComponent<FadeController>().isFading = true;
            }

            cinematicButton.image.sprite = eyeClosed;

            planetSelectorFadeCanvas.GetComponent<CanvasGroupController>().isFading = true;

            infoSelectorFadeCanvas.GetComponent<CanvasGroupController>().isFading = true;

            mainTextFadeCanvas.GetComponent<CanvasGroupController>().isFading = true;

            mainCamera.SetCinemaMode();
        }
        else
        {
            for (int i = 0; i < ringsHolder.transform.childCount; i++)
            {
                ringsHolder.transform.GetChild(i).GetComponent<FadeController>().isFading = false;
            }

            cinematicButton.image.sprite = eyeOpen;

            planetSelectorFadeCanvas.GetComponent<CanvasGroupController>().isFading = false;

            infoSelectorFadeCanvas.GetComponent<CanvasGroupController>().isFading = false;

            mainTextFadeCanvas.GetComponent<CanvasGroupController>().isFading = false;

            mainCamera.SetNonCinemode();
        }
    }

    void AssignText(int pIndex, int iIndex)
    {
        string textToOutput = "";

        textToOutput += planetCollectiveInfo[pIndex].name;

        switch (iIndex)
        {
            default:
                textToOutput += planetCollectiveInfo[planetIndex].Diameter;
                break;
            case 1:
                textToOutput += planetCollectiveInfo[planetIndex].DistanceFromSun;
                break;
            case 2:
                textToOutput += planetCollectiveInfo[planetIndex].OrbitDuration;
                break;
        }

        PlanetInfoText.text = textToOutput.ToString();
    }

    public void PlanetSelector(int index)
    {
        planetIndex = index;

        InfoSelector(infoIndex);
    }

    public void InfoSelector(int index)
    {
        infoIndex = index;

        AssignText(planetIndex, infoIndex);
    }

    public void RingRremoval()
    {
        if (ringsHolder.gameObject.activeInHierarchy)
        {
            ringsHolder.SetActive(false);
        }
        else
        {
            ringsHolder.SetActive(true);
        }
    }

    public void PlanetStopper()
    {
        if (planetContainer.transform.GetChild(0).GetComponent<RotationScript>().actualRotationSpeed == 0)
        {
            for (int i = 0; i < planetContainer.transform.childCount; i++)
            {
                RotationScript rotator = planetContainer.transform.GetChild(i).GetComponent<RotationScript>();
                rotator.actualRotationSpeed = rotator.baseSpeed;
            }
        }
        else
        {
            for (int i = 0; i < planetContainer.transform.childCount; i++)
            {
                RotationScript rotator = planetContainer.transform.GetChild(i).GetComponent<RotationScript>();
                rotator.actualRotationSpeed = 0;
            }
        }
    }
}

[System.Serializable]
public struct PlanetCollectiveInfo
{
    public string name;

    public string Diameter;

    public string DistanceFromSun;

    public string OrbitDuration;

    //internal stuff
    public float internalRadius;
    public FadeController fadeController;
}