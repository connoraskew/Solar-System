using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public bool isFading;

    public float fadeDuration = 0.5f;

    private SpriteRenderer spriteRend;

    public bool isBackground;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isFading)
        {
            float alphaToLerpTo = 0f;

            if (isBackground)
            {
                alphaToLerpTo = 0f;
            }
            else
            {
                alphaToLerpTo = 0.3f;
            }


            if (spriteRend.color.a > alphaToLerpTo)
            {
                Color color = spriteRend.color;

                color.a -= fadeDuration * Time.deltaTime;

                spriteRend.color = color;
            }
        }
        else
        {
            if (spriteRend.color.a < 0.5)
            {
                Color color = spriteRend.color;

                color.a += fadeDuration * Time.deltaTime;

                spriteRend.color = color;
            }
        }
    }
}