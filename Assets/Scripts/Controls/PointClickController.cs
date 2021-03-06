using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;


// TODO: Possilbe improvements: Left Click, Right Click, Middle Mouse Click
/// <summary>
/// This controller is used to handle the mouse events in perspective color mode and delegate them 
/// </summary>
public class PointClickController : MonoBehaviour
{
    public static PointClickController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<PointClickController>();
            }
            return instance;
        }
    }
    private static PointClickController instance;

    /// <summary>
    /// Target layers used for mouse pointer handling
    /// </summary>
    public LayerMask targetLayers;

    /// <summary>
    /// max mouse handling distance from camera to mousepointer
    /// </summary>
    public float distance = 1000;

    /// <summary>
    /// If false, mouse event handling is freezed
    /// </summary>
    public bool HandleControls = true;

    private Vector3 previousPosition;

    private bool mousePressed = false;

    private PointerInfo ClickDownObject = null;
    private Vector3 ClickDownPosition = PointerInfo.INVALID;

    private PointerInfo LastClickObject = null;
    private Vector3 LastClickPosition = PointerInfo.INVALID;

    [SerializeField]
    private float MaxDblClickTime = 0.5f;

    private float LastClickedTime = 0;

    private List<MouseEventHandler> handlers = new List<MouseEventHandler>();

    private void Awake()
    {
        if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void RegisterMouseEventHandler(MouseEventHandler handler)
    {
        if (!Instance.handlers.Contains(handler))
        {
            Instance.handlers.Add(handler);
        }
    }

    public void UnregisterMouseEventHandler(MouseEventHandler handler)
    {
        if (Instance.handlers.Contains(handler))
        {
            Instance.handlers.Remove(handler);
        }
    }

    /// <summary>
    /// Performs the raycast that is used to handle mouse events
    /// </summary>
    /// <param name="realmousepos"></param>
    /// <param name="info"></param>
    /// <returns>true if we hit an object that is on the target layer</returns>
    private bool Point(Vector3 realmousepos, out PointerInfo info)
    {
        Ray mousepos = Camera.main.ScreenPointToRay(realmousepos);

        RaycastHit hit;

        if (Physics.Raycast(mousepos, out hit, distance, targetLayers))
        {
            if (hit.transform != null)
            {
                MouseEventHandler receiver = hit.transform.GetComponent<MouseEventHandler>();

                if (receiver == null)
                {
                    Debug.DrawRay(mousepos.origin, mousepos.direction * distance, Color.yellow);

                    info = new PointerInfo(realmousepos, hit);

                }
                else
                {
                    Debug.DrawRay(mousepos.origin, mousepos.direction * distance, Color.green);
                    info = new PointerInfo(receiver, realmousepos, hit);
                }
                return true;
            }
            else
            {
                Debug.LogError("Error this case should not be valid, what happened?", this);
            }

        }
        else
        {
            Debug.DrawRay(mousepos.origin, mousepos.direction * distance, Color.red);
        }
        info = null;
        return false;
    }

    /// <summary>
    ///  Handle mousedown event
    /// </summary>
    /// <param name="info"></param>
    private void handleMouseDown(PointerInfo info)
    {
        mousePressed = true;

        ClickDownObject = info;
        ClickDownPosition = info.HitPos;

        foreach (MouseEventHandler handler in handlers)
        {
            handler.HandleMouseDown(info);
        }
    }

    /// <summary>
    /// Handle mouseup event
    /// </summary>
    /// <param name="info"></param>
    private void handleMouseUp(PointerInfo info)
    {
        mousePressed = false;

        if (LastClickObject != null && LastClickObject.HitTransform == info.HitTransform) // Double Click
        {
            // Check time
            if ((Time.time - LastClickedTime) <= MaxDblClickTime)
            {
                foreach (MouseEventHandler handler in handlers)
                {
                    handler.HandleMouseClick2(info);
                }
                LastClickObject = null;
                LastClickedTime = 0;
            }
            else if (ClickDownObject != info)
            {
                handleDrop(info);
            }
            else
            {
                foreach (MouseEventHandler handler in handlers)
                {
                    handler.HandleMouseClick1(info);
                }
                LastClickObject = info;
                LastClickedTime = Time.time;
            }

        }
        else
        {
            handleDrop(info);

            // Reset LastClickObject

            if (ClickDownObject.HitTransform == info.HitTransform) // Single Click
            {
                LastClickObject = ClickDownObject;
                LastClickedTime = Time.time;
            }
            else // Drop (not implemented)
            {
                LastClickObject = null;
                LastClickedTime = 0;
            }
        }

        ClickDownObject = null;
        ClickDownPosition = PointerInfo.INVALID;
    }

    /// <summary>
    /// Handles dropping 
    /// </summary>
    /// <param name="info"></param>
    private void handleDrop(PointerInfo info)
    {
        foreach (MouseEventHandler handler in handlers)
        {
            handler.HandleMouseUp(info);
            if (ClickDownObject.HitTransform == info.HitTransform) // Single Click
            {
                handler.HandleMouseClick1(info);
                LastClickedTime = Time.time;
            }
            else // Drop
            {
                handler.HandleDrop(ClickDownObject, info);
            }
        }
    }

    /// <summary>
    /// Handles mouse hold and draging
    /// </summary>
    /// <param name="info"></param>
    private void handleMouseHold(PointerInfo info)
    {
        if (ClickDownObject.HitTransform == info.HitTransform) // Hold
        {
            foreach (MouseEventHandler handler in handlers)
            {
                handler.HandleMouseHold(info);
            }
        }
        else // Drag
        {
            foreach (MouseEventHandler handler in handlers)
            {
                handler.HandleDrag(ClickDownObject, info);
            }
        }
    }

    /// <summary>
    /// Handles mouse hovering event
    /// </summary>
    /// <param name="info"></param>
    private void handleMouseHover(PointerInfo info)
    {
        foreach (MouseEventHandler handler in handlers)
        {
            handler.HandleMouseHover(info);
        }
    }

    /// <summary>
    /// Handles click events
    /// </summary>
    private void handleMouseInput()
    {
        PointerInfo info = null;
        if (Point(Input.mousePosition, out info))
        {
            if (Input.GetMouseButtonDown(0)) // Mouse Down
            {
                handleMouseDown(info);
            }
            else if (Input.GetMouseButtonUp(0)) // Mouse Up
            {
                handleMouseUp(info);
            }
            else if (Input.GetMouseButton(0)) // Mouse Hold
            {
                handleMouseHold(info);
            }
            else // Hover
            {
                handleMouseHover(info);
            }
        }
    }

    private void Update()
    {
        if (HandleControls)
        {
            handleMouseInput();
        }
    }

}
