using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // How fast our player can move in both horizontal and vertical directions
    public Vector2 speed = new Vector2(1, 1);
    // Vector2 to hold our movement data
    Vector2 playerMovement;
    // GameObject for our camera
    public GameObject mainCamera;
    // How fast we want the camera to move once we hit the edge
    // Slower = smoother, Faster = snappier
    public float cameraSpeed = 1.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        //Debug.Log(viewPos);

        if (viewPos.x >= 0.95f)
        {
            // Smoothly catch up
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z), cameraSpeed * Time.deltaTime);
            // Snap to next screen
            //mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
            Debug.Log("Right");
        }

        if (viewPos.x <= 0.05f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z), cameraSpeed * Time.deltaTime);
            //mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
            Debug.Log("Left");
        }

        if (viewPos.y <= 0.1f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z), cameraSpeed * Time.deltaTime);
            //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
            Debug.Log("Top");
        }

        if (viewPos.y >= 0.9f)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z), cameraSpeed * Time.deltaTime);
            //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
            Debug.Log("Bottom");
        }
        
        // Get our horizontal and vertical inputs and multiply them by a speed variable
        playerMovement = new Vector2(speed.x * Input.GetAxis("Horizontal"), speed.y * Input.GetAxis("Vertical"));
	}

    void FixedUpdate()
    {
        //  Move our object
        rigidbody2D.velocity = playerMovement;
    }
}
