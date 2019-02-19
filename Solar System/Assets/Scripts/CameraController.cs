using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform desiredPos; 

    public Transform cinemaMode; 
    public Transform nonCinemaMode;

    public float lerpSpeed;

    void Start()
    {
        desiredPos = nonCinemaMode;
        transform.position = nonCinemaMode.position;
        transform.rotation = nonCinemaMode.rotation;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPos.position, lerpSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredPos.rotation, lerpSpeed * Time.deltaTime);
    }

    public void SetCinemaMode()
    {
        desiredPos = cinemaMode;
    }

    public void SetNonCinemode()
    {
        desiredPos = nonCinemaMode;
    }
}