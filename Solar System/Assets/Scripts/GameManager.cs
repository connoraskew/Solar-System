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

    private Material previousSelectedPlanetsMaterial;
    private Material currentSelectedPlanetsMaterial;

    [Range(0,1)] public float highlightPercent;

    private Camera viewCam;

    [SerializeField] private PlanetTexts[] planetTexts;
    int randomTextToSpawn;

    void Awake()
    {
        isCinematicMode = false;

        viewCam = Camera.main;

        CinemaSetUp();

        PlanetSelector(0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        Ray ray = viewCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            Color newColour = new Color(highlightPercent, highlightPercent, highlightPercent, 1);

            GameObject currentPlanet = hit.collider.gameObject;

            if (hit.collider.gameObject.name != "Sun")
            {
                currentSelectedPlanetsMaterial = currentPlanet.GetComponent<MeshRenderer>().material;

                if (currentSelectedPlanetsMaterial.GetColor("_EmissionColor") != newColour)
                {
                    currentSelectedPlanetsMaterial.SetColor("_EmissionColor", newColour);
                }
            }

            int planetTextIndex = currentPlanet.GetComponent<PlanetIndexer>().planetIndex;

            if (randomTextToSpawn == -1)
            {
                randomTextToSpawn = Random.Range(0, planetTexts[planetTextIndex].textBoxes.Length);
                Debug.Log(randomTextToSpawn);
            }

        }
        else
        {
            if (currentSelectedPlanetsMaterial != null)
            {
                Color newColour = new Color(0, 0, 0, 1);
                currentSelectedPlanetsMaterial.SetColor("_EmissionColor", newColour);
                currentSelectedPlanetsMaterial = null;
            }

            randomTextToSpawn = -1;
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
}

[System.Serializable]
public struct PlanetTexts
{
    public string name;

    public GameObject[] textBoxes;
}
