using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupController : MonoBehaviour
{
    public bool isFading;

    public float fadeDuration = 0.5f;

    float actualFadeDuration = 0.5f;

    CanvasGroup canvasGroup;

    // Use this for initialization
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        actualFadeDuration = 1 / fadeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * actualFadeDuration;
            }
        }
        else
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime * actualFadeDuration;
            }
        }
    }
}
