using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouseEventHandler : MonoBehaviour
{
    
    public abstract void HandleMouseClick1(PointerInfo info);
    public abstract void HandleMouseClick2(PointerInfo info);
    public abstract void HandleMouseDown(PointerInfo info);
    public abstract void HandleMouseUp(PointerInfo info);
    public abstract void HandleMouseHold(PointerInfo info);
    public abstract void HandleMouseHover(PointerInfo info);
    public abstract void HandleDrag(PointerInfo drag, PointerInfo drop);
    public abstract void HandleDrop(PointerInfo drag, PointerInfo drop);
}
