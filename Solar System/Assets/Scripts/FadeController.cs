using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public bool isFading;

    public float fadeDuration = 0.5f;

    private SpriteRenderer spriteRend;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isFading)
        {
            if (spriteRend.color.a > 0)
            {
                Color color = spriteRend.color;

                color.a -= fadeDuration * Time.deltaTime;

                spriteRend.color = color;
            }
        }
        else
        {
            if (spriteRend.color.a < 1)
            {
                Color color = spriteRend.color;

                color.a += fadeDuration * Time.deltaTime;

                spriteRend.color = color;
            }
        }
    }
}