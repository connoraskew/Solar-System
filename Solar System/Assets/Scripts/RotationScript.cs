using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{

    public float rotationSpeed;
    private float actualRotationSpeed;
    public float dampAmount;

    public bool isPlanet;

    void Start()
    {
        actualRotationSpeed = 100 / -rotationSpeed;
        if (isPlanet)
        {
            float yRotation = Random.Range(0f, 360f);

            Vector3 newRotation = new Vector3(transform.rotation.x, yRotation, transform.rotation.z);

            transform.eulerAngles = newRotation;

            Debug.Log(yRotation + " | " + transform.rotation.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate((Vector3.up * actualRotationSpeed) * (Time.deltaTime * dampAmount), Space.World);
    }
}