using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Vector2 speed = new Vector2(1, 1);
    Vector2 playerMovement;
    public GameObject mainCamera;
    public float cameraSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        //Debug.Log(transform.renderer.bounds.size.x + "," + transform.renderer.bounds.size.y);
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

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        playerMovement = new Vector2(speed.x * inputX, speed.y * inputY);

	}

    void FixedUpdate()
    {
        rigidbody2D.velocity = playerMovement;
    }
}
