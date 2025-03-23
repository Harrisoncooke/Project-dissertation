using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f; // Speed at which the camera pans
    public float zoomSpeed = 5f; // Speed at which the camera zooms
    public float minZoom = 5f; // Minimum zoom level
    public float maxZoom = 20f; // Maximum zoom level

    void Update()
    {
        HandleMovement(); // Handle camera panning
        HandleZoom(); // Handle camera zooming
    }

    void HandleMovement()
    {
        Vector3 pos = transform.position;

        // Move the camera up
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        // Move the camera down
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        // Move the camera left
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        // Move the camera right
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        transform.position = pos; // Update the camera position
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // Get scroll input
        Vector3 pos = transform.position;

        // Adjust the camera's z position based on scroll input
        pos.z += scroll * zoomSpeed * 100f * Time.deltaTime;
        // Clamp the zoom level to stay within min and max zoom limits
        pos.z = Mathf.Clamp(pos.z, -maxZoom, -minZoom);

        transform.position = pos; // Update the camera position
    }
}

