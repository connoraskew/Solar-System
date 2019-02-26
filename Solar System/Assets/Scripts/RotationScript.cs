using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float rotationSpeed;
    public float actualRotationSpeed;
    public float dampAmount;

    public bool isPlanet;

    public float baseSpeed;

    void Start()
    {
        actualRotationSpeed = 100 / -rotationSpeed;
        baseSpeed = actualRotationSpeed;

        if (isPlanet)
        {
            float yRotation = Random.Range(0f, 360f);

            Vector3 newRotation = new Vector3(transform.rotation.x, yRotation, transform.rotation.z);

            transform.eulerAngles = newRotation;
        }
    }

    void Update()
    {
        transform.Rotate((Vector3.up * actualRotationSpeed) * (Time.deltaTime * dampAmount), Space.Self);
    }
}