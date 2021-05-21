using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TestClickEventHandler : MouseEventHandler
{
    public override void HandleDrag(PointerInfo drag, PointerInfo drop)
    {
        Debug.Log($"Drag {drag.HitTransform.name} on {drop.HitTransform.name}", gameObject);
    }

    public override void HandleDrop(PointerInfo drag, PointerInfo drop)
    {
        Debug.Log($"Drop {drag.HitTransform.name} on {drop.HitTransform.name}", gameObject);
    }

    public override void HandleMouseClick1(PointerInfo info)
    {
        Debug.Log($"Click1 {info.HitTransform.name}", gameObject);
    }

    public override void HandleMouseClick2(PointerInfo info)
    {
        Debug.Log($"Click2 {info.HitTransform.name}", gameObject);
    }

    public override void HandleMouseDown(PointerInfo info)
    {
        Debug.Log($"Down {info.HitTransform.name}", gameObject);
    }

    public override void HandleMouseHold(PointerInfo info)
    {
        Debug.Log($"Hold {info.HitTransform.name}", gameObject);
    }

    public override void HandleMouseHover(PointerInfo info)
    {
        Debug.Log($"Hover {info.HitTransform.name}", gameObject);
    }

    public override void HandleMouseUp(PointerInfo info)
    {
        Debug.Log($"Up {info.HitTransform.name}", gameObject);
    }
}
