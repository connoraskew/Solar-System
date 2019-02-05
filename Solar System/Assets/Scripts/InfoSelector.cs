using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSelector : MonoBehaviour
{
    int currentInfo;

    float desiredPosition;

    public RectTransform scrollUI;

    public float scrollTime;

    public float imageWidthAndGap;

    int maxInfo;

    public float cushion;

    void Start()
    {
        currentInfo = 0;

        desiredPosition = scrollUI.anchoredPosition3D.x;

        maxInfo = scrollUI.childCount - 3;
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
            if (currentInfo < maxInfo)
            {
                desiredPosition -= imageWidthAndGap;

                currentInfo++;
            }
        }
        else
        {
            if (currentInfo > 0)
            {
                desiredPosition += imageWidthAndGap;

                currentInfo--;
            }
        }

        Debug.Log(currentInfo + " | " + maxInfo);
    }
}
