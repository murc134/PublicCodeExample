using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dragable cube that is used to change the color of drop area when dropping it there
/// </summary>
public class ColorCube : MouseEventHandler
{
    private BoxCollider boxCollider;

    private Vector3 startPosition;

    private MeshRenderer meshRenderer;

    private Color defaultColor;

    /// <summary>
    /// Current color of this cubes renderer material
    /// </summary>
    public Color Color { 
        get { return meshRenderer.material.GetColor("_Color"); }
        private set { meshRenderer.material.SetColor("_Color", value); }
    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        startPosition = transform.position;
        defaultColor = Color;
    }

    /// <summary>
    /// When dragging position cube on mouse pointer and deactivate boxcollider to be able to recognize dropping
    /// </summary>
    /// <param name="drag"></param>
    /// <param name="drop"></param>
    public override void HandleDrag(PointerInfo drag, PointerInfo drop)
    {
        if (drag.HitTransform == transform)
        {
            boxCollider.enabled = false;
            transform.position = new Vector3(drop.HitPos.x, transform.position.y, drop.HitPos.z);
        }
    }

    /// <summary>
    /// When dropped, reset box collider to enabled, to be able to drag again
    /// </summary>
    /// <param name="drag"></param>
    /// <param name="drop"></param>
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

    /// <summary>
    /// On double click randomize cube color
    /// </summary>
    /// <param name="info"></param>
    public override void HandleMouseClick2(PointerInfo info)
    {
        if(info.HitGameObject == gameObject)
        {
            RandomColor();
        }
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

    /// <summary>
    /// Reset the position of the cube to the start position
    /// </summary>
    public void ResetPosition()
    {
        transform.position = startPosition;
        boxCollider.enabled = true;
    }

    /// <summary>
    /// Reset cube color to start color
    /// </summary>
    public void ResetColor()
    {
        Color = defaultColor;
    }

    /// <summary>
    /// Randomize cube color. Make sure that the new color value is not close to white color
    /// </summary>
    public void RandomColor()
    {
        Color c = Color.Randomize();

        if (c.Similarity(Color.white) > 0.5f)
        {
            RandomColor();
        }
        else
        {
            Color = c;
        }
        
    }
}
