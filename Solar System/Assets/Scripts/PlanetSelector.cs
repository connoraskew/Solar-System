using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelector : MonoBehaviour
{
    int currentPlanet;

    float desiredPosition;

    public RectTransform scrollUI;

    public float scrollTime;

    public float imageWidthAndGap;

    int maxPlanet;

    public float cushion;

    void Start()
    {
        currentPlanet = 0;

        desiredPosition = scrollUI.anchoredPosition3D.x;

        maxPlanet = scrollUI.childCount - 3;
    }

    void Update()
    {
        if (desiredPosition < scrollUI.anchoredPosition3D.x - cushion)
        {
            scrollUI.transform.Translate(-Vector3.right * Time.deltaTime * scrollTime);
        }
        else if (desiredPosition > scrollUI.anchoredPosition3D.x + cushion)
        {
            scrollUI.transform.Translate(Vector3.right * Time.deltaTime * scrollTime);
        }
    }

    public void SetAnchorPosition(bool _add)
    {
        if (_add)
        {
            if (currentPlanet < maxPlanet)
            {
                desiredPosition -= imageWidthAndGap;

                currentPlanet++;
            }
        }
        else
        {
            if (currentPlanet > 0)
            {
                desiredPosition += imageWidthAndGap;

                currentPlanet--;
            }
        }

        Debug.Log(currentPlanet + " | " + maxPlanet);
    }
}