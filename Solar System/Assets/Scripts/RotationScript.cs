using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{

    public float rotationSpeed;
    private float actualRotationSpeed;
    public float dampAmount;

    void Start()
    {
        actualRotationSpeed = 100 / rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate((Vector3.up * actualRotationSpeed) * (Time.deltaTime * dampAmount), Space.Self);
    }
}