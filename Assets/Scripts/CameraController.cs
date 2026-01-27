using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    /* singleton script that manages camera controls */
    public static CameraController instance;
    private Camera camera;
    private InputAction moveAction;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float maximumX;
    [SerializeField] private float minimumX;
    [SerializeField] private float maximumY;
    [SerializeField] private float minimumY;
    private bool isMoving;

    private void Awake()
    {
        isMoving = false;
        instance = this;
        camera = GetComponent<Camera>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        // other conditions that prevent camera freedom should be added here
        if (!isMoving)
        {
            // move camera at a consistent speed relative to player input
            Vector2 input = moveAction.ReadValue<Vector2>();
            Vector3 movementHere = new Vector3(input.x, input.y, 0);
            transform.Translate(movementHere * (cameraSpeed * Time.deltaTime));
            
            // keep camera within bounds
            if (transform.position.x < minimumX)
            {
                transform.position = new Vector3(minimumX, transform.position.y, transform.position.z);
            }  else if (transform.position.x > maximumX) 
            {
                transform.position = new Vector3(maximumX, transform.position.y, transform.position.z);
            }

            if (transform.position.y < minimumY)
            {
                transform.position = new Vector3(transform.position.x, minimumY, transform.position.z);
            }   else if (transform.position.y > maximumY) 
            {
                transform.position = new Vector3(transform.position.x, maximumY, transform.position.z);
            }
        }
    }

    public void GoToTile(TileManager targetTile)
    {
        if (!isMoving)
        {
            StartCoroutine(GoToObject(targetTile.gameObject));
        } else Debug.Log("already moving");
    }

    IEnumerator GoToObject(GameObject targetObject)
    {
        Vector3 targetPosition = targetObject.transform.position;
        isMoving = true;
        Vector3 distanceRemaining = transform.position - targetPosition;
        // gets 'close enough' change values in editor to alter speed and accuracy of camera movement
        float goToTime = 0.8f;
        Vector3 camVelocity = Vector3.zero;
        while (distanceRemaining.sqrMagnitude > 4 && isMoving)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref camVelocity, goToTime);
            distanceRemaining = transform.position - targetPosition;
            yield return null;
        }
        isMoving = false;
    }
}
