using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EventHandlerScript : MonoBehaviour
{
    public static EventHandlerScript Instance;
    public UnityEvent hotbarSwitchEvent;
    public Camera mainCamera;

    private void Awake()
    {
        Instance = this;
        hotbarSwitchEvent =  new UnityEvent();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider)
        {
            Debug.Log("no click target");
            return;
        }
        Debug.Log(rayHit.collider.gameObject.name);
        TileScript tileHit = rayHit.collider.gameObject.GetComponent<TileScript>();
        tileHit?.OnClick();
    }
}
