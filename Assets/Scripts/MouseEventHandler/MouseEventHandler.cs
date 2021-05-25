using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstact class for mouse event handling
/// </summary>
public abstract class MouseEventHandler : MonoBehaviour
{
    /// <summary>
    /// Makes sure that the <see cref="MouseEventHandler"/> registers its presence, if a <see cref="PointClickController"/> exist
    /// </summary>
    private void OnEnable()
    {
        if(PointClickController.Instance != null)
        {
            PointClickController.Instance.RegisterMouseEventHandler(this);
        }
        else
        {
            Debug.LogError($"No instance pf {typeof(PointClickController).Name} exists");
        }
    }

    /// <summary>
    /// Makes sure that the <see cref="MouseEventHandler"/> unregisters its presence, if a <see cref="PointClickController"/> exist
    /// </summary>
    private void OnDestroy()
    {
        if(PointClickController.Instance != null)
        {
            PointClickController.Instance.UnregisterMouseEventHandler(this);
        }
    }

    public abstract void HandleMouseClick1(PointerInfo info);
    public abstract void HandleMouseClick2(PointerInfo info);
    public abstract void HandleMouseDown(PointerInfo info);
    public abstract void HandleMouseUp(PointerInfo info);
    public abstract void HandleMouseHold(PointerInfo info);
    public abstract void HandleMouseHover(PointerInfo info);
    public abstract void HandleDrag(PointerInfo drag, PointerInfo drop);
    public abstract void HandleDrop(PointerInfo drag, PointerInfo drop);
}
