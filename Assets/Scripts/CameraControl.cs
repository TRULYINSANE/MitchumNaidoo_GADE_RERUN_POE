using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float CamSpeed = 10;
    public float targetOrthog;

    // Use this for initialization
    void Start ()
    {
        targetOrthog = Camera.main.orthographicSize;

    }

    // Update is called once per frame
    void Update ()
    {
        MoveMouse(CamSpeed);
        ZoomMouse();
        transform.Translate(Input.GetAxis("Horizontal") * CamSpeed * Time.deltaTime, Input.GetAxis("Vertical") * CamSpeed * CamSpeed * Time.deltaTime, 0);
    }

    void MoveMouse(float speed)
    {
        transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0f);
    }

    void ZoomMouse()
    {
        float speedZoom = 4;
        float speedSmo = 5f;
        float min = 1f;
        float max = 25f;

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            targetOrthog -= scroll * speedZoom;
            targetOrthog = Mathf.Clamp(targetOrthog, min, max);
        }
        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrthog, speedSmo * Time.deltaTime);
    }
}