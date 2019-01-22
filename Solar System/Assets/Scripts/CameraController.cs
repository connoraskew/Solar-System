using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    private float panBoarderThickness;
    public float panBoarderThicknessPercent = 10f;
    public Vector2 panLimit;

    public float cameraLerpSpeed;

    public float scrollSpeed = 20f;
    public float minY = 20f;
    public float maxY = 120f;

    public Vector3 offset;
    Camera viewCamera;

    bool planetSelected;
    public LayerMask planetLayerMask;
    public GameObject selectedPlanet;


    // Use this for initialization
    void Start()
    {
        panBoarderThickness = Screen.height / panBoarderThicknessPercent;
        // using vector3.zero because it is the centre of what the camera is looking at
        offset = transform.position;

        viewCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetMouseButton(0))
        {
            PlanetSelector();
        }
    }

    void Movement()
    {
        Vector3 pos = transform.position;




        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow) /* || Input.mousePosition.y >= Screen.height - panBoarderThickness*/)
        {
            pos.z += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) /* || Input.mousePosition.y <= panBoarderThickness*/)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) /* || Input.mousePosition.x >= Screen.width - panBoarderThickness*/)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) /* || Input.mousePosition.x <= panBoarderThickness*/)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }



        if (Input.GetKeyDown("space"))
        {
            planetSelected = false;
            selectedPlanet = null;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = offset;
        }
    }

    void PlanetSelector()
    {
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float rayDistance = 1000f;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            planetSelected = true;
            selectedPlanet = hit.transform.gameObject;
        }
    }
}