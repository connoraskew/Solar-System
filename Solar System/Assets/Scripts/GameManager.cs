using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// make a quiz
// add an info button which explains how to use and what each of the buttons do

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isCinematicMode;

    [SerializeField] private bool isInfoMode;

    [SerializeField] private GameObject planetContainer;

    [SerializeField] private GameObject ringsHolder;

    [SerializeField] private GameObject planetSelectorFadeCanvas;

    [SerializeField] private GameObject infoSelectorFadeCanvas;

    [SerializeField] private GameObject mainTextFadeCanvas;

    [SerializeField] private GameObject infoFadeCanvas;
    [SerializeField] private Scrollbar infoScrollBar;

    //[SerializeField] private GameObject UIRingsHolder;

    [SerializeField] private CameraController mainCamera;

    [SerializeField] private Sprite eyeClosed;
    [SerializeField] private Sprite eyeOpen;

    [SerializeField] private Button cinematicButton;

    [SerializeField] private PlanetCollectiveInfo[] planetCollectiveInfo;

    [SerializeField] private Text PlanetInfoText;

    [SerializeField] private int planetIndex;
    [SerializeField] private int infoIndex;


    private Vector3 hitPos;
    private Vector3 clickedPos;
    [SerializeField] private float mouseTollerance;
    private GameObject activeTextBox;


    private Material previousSelectedPlanetsMaterial;
    private Material currentSelectedPlanetsMaterial;
    private GameObject currentSelectedPlanet;

    [Range(0, 1)] public float highlightPercent;

    private Camera viewCam;

    [SerializeField] private PlanetTexts[] planetTexts;
    int randomTextToSpawn;

    void Awake()
    {
        isCinematicMode = false;

        isInfoMode = false;

        clickedPos = new Vector3(999, 999, 999);

        viewCam = Camera.main;

        mouseTollerance = 25f;

        randomTextToSpawn = -1;

        CinemaSetUp();

        PlanetSelector(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        MouseRayCast();

        InfoTextBoxes();
    }

    void InfoTextBoxes()
    {

        if (clickedPos != new Vector3(999, 999, 999))
        {
            Ray ray = viewCam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                hitPos = ray.GetPoint(rayDistance);

                float mouseDistance = Vector3.Distance(hitPos, clickedPos);

                if (mouseDistance >= mouseTollerance)
                {
                    mainTextFadeCanvas.GetComponent<CanvasGroupController>().isFading = false;

                    activeTextBox.GetComponent<CanvasGroupController>().isFading = true;

                    clickedPos = new Vector3(999, 999, 999);
                }
            }
        }
    }

    void MouseRayCast()
    {
        Ray ray = viewCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            currentSelectedPlanet = hit.collider.gameObject;

            currentSelectedPlanetsMaterial = currentSelectedPlanet.GetComponent<MeshRenderer>().material;

            if (hit.collider.gameObject.name != "Sun")
            {
                Color newColour = new Color(highlightPercent, highlightPercent, highlightPercent, 1);

                if (currentSelectedPlanetsMaterial.GetColor("_EmissionColor") != newColour)
                {
                    currentSelectedPlanetsMaterial.SetColor("_EmissionColor", newColour);
                }
            }
            else
            {
                Color newColour = new Vector4(0.749f, 0.333f, 0.333f, 1f);

                if (currentSelectedPlanetsMaterial.GetColor("_EmissionColor") != newColour)
                {
                    currentSelectedPlanetsMaterial.SetColor("_EmissionColor", newColour);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                mainTextFadeCanvas.GetComponent<CanvasGroupController>().isFading = true;

                int planetTextIndex = currentSelectedPlanet.GetComponent<PlanetIndexer>().planetIndex;

                if (randomTextToSpawn == -1)
                {
                    randomTextToSpawn = Random.Range(0, planetTexts[planetTextIndex].textBoxes.Length);

                    //GameObject textToActivate = planetTexts[planetTextIndex].textBoxes[randomTextToSpawn];
                    activeTextBox = planetTexts[planetTextIndex].textBoxes[randomTextToSpawn];

                    activeTextBox.GetComponent<CanvasGroupController>().isFading = false;

                    //activeTextBox = textToActivate;

                    RectTransform myRectTransform = activeTextBox.GetComponent<RectTransform>();

                    myRectTransform.position = Input.mousePosition;

                    if (Input.mousePosition.x < Screen.width * 0.5)
                    {
                        myRectTransform.localPosition += Vector3.right * (myRectTransform.rect.width * 0.6f);
                    }
                    else
                    {
                        myRectTransform.localPosition -= Vector3.right * (myRectTransform.rect.width * 0.6f);
                    }

                    myRectTransform.localPosition += Vector3.up * (myRectTransform.rect.height * 0.6f);
                }

                clickedPos = hit.point;
            }
        }
        else
        {
            if (currentSelectedPlanet != null)
            {
                if (currentSelectedPlanet.name != "Sun")
                {

                    if (currentSelectedPlanetsMaterial != null)
                    {
                        Color newColour = new Color(0, 0, 0, 1);

                        currentSelectedPlanetsMaterial.SetColor("_EmissionColor", newColour);

                        currentSelectedPlanetsMaterial = null;
                    }
                }
                else
                {
                    if (currentSelectedPlanetsMaterial != null)
                    {
                        Color newColour = new Color(0.549f, 0.133f, 0.133f, 1f);

                        currentSelectedPlanetsMaterial.SetColor("_EmissionColor", newColour);

                        currentSelectedPlanetsMaterial = null;
                    }
                }
                currentSelectedPlanet = null;

                randomTextToSpawn = -1;
            }
        }
    }

    public void CinematicButtonPress()
    {
        isCinematicMode = !isCinematicMode;

        CinemaSetUp();
    }

    public void InfoButtonPress()
    {
        isInfoMode = !isInfoMode;

        InfoSetUp();
    }

    void InfoSetUp()
    {
        if (isInfoMode)
        {
            mainTextFadeCanvas.GetComponent<CanvasGroupController>().isFading = true;

            infoFadeCanvas.GetComponent<CanvasGroupController>().isFading = false;

            infoScrollBar.value = 1.0f;
        }
        else
        {
            mainTextFadeCanvas.GetComponent<CanvasGroupController>().isFading = false;

            infoFadeCanvas.GetComponent<CanvasGroupController>().isFading = true;
        }    
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
