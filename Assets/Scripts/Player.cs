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

    void OnEnable()
    {
        StartCoroutine(PostFixedUpdate());
    }

    IEnumerator PostFixedUpdate()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            updateCamera();
        }
    }

	// Update is called once per frame
	void Update () {
        //Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        ////Debug.Log(viewPos);

        //if (viewPos.x >= 0.95f)
        //{
        //    // Smoothly catch up
        //    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z), cameraSpeed * Time.deltaTime);
        //    // Snap to next screen
        //    //mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        //    Debug.Log("Right");
        //}

        //if (viewPos.x <= 0.05f)
        //{
        //    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z), cameraSpeed * Time.deltaTime);
        //    //mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        //    Debug.Log("Left");
        //}

        //if (viewPos.y <= 0.1f)
        //{
        //    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z), cameraSpeed * Time.deltaTime);
        //    //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
        //    Debug.Log("Top");
        //}

        //if (viewPos.y >= 0.9f)
        //{
        //    mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z), cameraSpeed * Time.deltaTime);
        //    //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
        //    Debug.Log("Bottom");
        //}
        
        // Get our horizontal and vertical inputs and multiply them by a speed variable
        playerMovement = new Vector2(speed.x * Input.GetAxis("Horizontal"), speed.y * Input.GetAxis("Vertical"));
	}

    public float horizontalThreshold = 0.4f;
    public float verticalThreshold = 0.4f;

    void updateCamera()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        Debug.Log("viewPos = " + viewPos.ToString());
        //Debug.Log(viewPos);

        bool dirty = false;

        if (viewPos.x > (1f - horizontalThreshold))
        {
            viewPos.x = (1f - horizontalThreshold);
            Debug.Log("Right");
            dirty = true;
        }

        if (viewPos.x < horizontalThreshold)
        {
            viewPos.x = horizontalThreshold;
            Debug.Log("Left");
            dirty = true;
        }

        if (viewPos.y < verticalThreshold)
        {
            viewPos.y = verticalThreshold;
            dirty = true;
        }

        if (viewPos.y > (1f - verticalThreshold))
        {
            viewPos.y = (1f - verticalThreshold);
            dirty = true;
        }

        if (dirty)
        {
            // figure out how much the camera needs to move so that the player will be at the updated viewport location
            Vector3 offset = transform.position - Camera.main.ViewportToWorldPoint(viewPos);
            mainCamera.transform.position += offset;
        }
    }

    void FixedUpdate()
    {
        //  Move our object
        rigidbody2D.velocity = playerMovement;
    }
}
