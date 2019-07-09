using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;

    public float scrollSpeed = 20f;

    private float PixelPerfectClamp(float zoom, float pixelsPerUnit) // Gör så att spelaren rör sig i pixel perfect units
    {

        float zoomInPixels = Mathf.RoundToInt((zoom * pixelsPerUnit));

        return zoomInPixels / pixelsPerUnit;
    }

    // Update is called once per frame
    void Update () {
        Vector3 pos = transform.position;

		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness) {
            pos.y += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness) {
            pos.y -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float zoom = GetComponent<Camera>().orthographicSize;
        float zoomChangeAmount = 5f;

        if (Input.mouseScrollDelta.y > 0) {
            zoom -= zoomChangeAmount * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y < 0) {
            zoom += zoomChangeAmount * Time.deltaTime;
        }

        zoom = Mathf.Clamp(zoom, 4f, 50f);
        zoom = PixelPerfectClamp(zoom, 64f);
        Debug.Log(string.Format("ZOOM IS " + zoom));

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        GetComponent<Camera>().orthographicSize = zoom;
        transform.position = pos;
	}
}
