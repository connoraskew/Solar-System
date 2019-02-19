using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// make a quiz

public class GameManager : MonoBehaviour
{
    public bool isCinematicMode;

    public GameObject background;
    public GameObject ringsHolder;
    public GameObject planetSelectorFadeCanvas;
    public GameObject infoSelectorFadeCanvas;
    public GameObject mainTextFadeCanvas;

    public GameObject UIRingsHolder;

    public CameraController mainCamera;
    Camera viewCam;

    [SerializeField] private Sprite eyeClosed;
    [SerializeField] private Sprite eyeOpen;

    public Button cinematicButton;

    public PlanetCollectiveInfo[] planetCollectiveInfo;

    public Text PlanetInfoText;

    public int planetIndex;
    public int infoIndex;

    [SerializeField] private LayerMask ringsLayer;
    [SerializeField] private LayerMask planetsLayer;

    void Awake()
    {
        isCinematicMode = false;

        viewCam = Camera.main;

        background.GetComponent<FadeController>().isFading = true;

        CinemaSetUp();

        PlanetSelector(0);
    }

    void Update()
    {
        RaycastHit hit;

        Ray ray = viewCam.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);

            Debug.DrawLine(ray.origin, point, Color.red);

            float hitDistance = Vector3.Distance(Vector3.zero, point);

            int currentSelectedPlanet = -1;

            for (int i = 0; i < planetCollectiveInfo.Length; i++)
            {
                if (hitDistance <= planetCollectiveInfo[i].internalRadius)
                {

                    if (currentSelectedPlanet == -1 || planetCollectiveInfo[i].internalRadius < planetCollectiveInfo[currentSelectedPlanet].internalRadius)
                    {
                        currentSelectedPlanet = i;
                    }
                }
            }
            
            for (int i = 0; i < planetCollectiveInfo.Length; i++)
            {
                if (i == currentSelectedPlanet)
                {
                    planetCollectiveInfo[i].fadeController.isFading = false;
                }
                else
                {
                    planetCollectiveInfo[i].fadeController.isFading = true;
                }
            }
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
            background.GetComponent<FadeController>().isFading = false;

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
            background.GetComponent<FadeController>().isFading = true;

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