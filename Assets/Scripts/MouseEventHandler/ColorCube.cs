using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCube : MouseEventHandler
{
    private BoxCollider boxCollider;

    private Vector3 startPosition;

    private MeshRenderer meshRenderer;

    public Color Color { get { return meshRenderer.material.GetColor("_Color"); } }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    public override void HandleDrag(PointerInfo drag, PointerInfo drop)
    {
        if (drag.HitTransform == transform)
        {
            boxCollider.enabled = false;
            transform.position = new Vector3(drop.HitPos.x, transform.position.y, drop.HitPos.z);
        }
    }

    public override void HandleDrop(PointerInfo drag, PointerInfo drop)
    {
        if(drag.HitTransform == transform)
        {
            boxCollider.enabled = true;
        }
    }

    public override void HandleMouseClick1(PointerInfo info)
    {

    }

    public override void HandleMouseClick2(PointerInfo info)
    {

    }

    public override void HandleMouseDown(PointerInfo info)
    {

    }

    public override void HandleMouseHold(PointerInfo info)
    {

    }

    public override void HandleMouseUp(PointerInfo info)
    {

    }
    public override void HandleMouseHover(PointerInfo info)
    {

    }
    public void ResetPosition()
    {
        transform.position = startPosition;
        boxCollider.enabled = true;
    }
}
