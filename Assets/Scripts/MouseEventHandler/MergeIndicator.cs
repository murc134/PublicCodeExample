using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Element that will preview the color merge
/// </summary>
public class MergeIndicator : MouseEventHandler
{
    // The graphics used for this UI element
    [SerializeField]
    private Image clickImg, hoverImg, mergeImg, dragImg, dropImg;

    /// <summary>
    /// When dragging a cube, show preview of drag object
    /// While dragging over target area also show target area color and merge color preview
    /// </summary>
    /// <param name="drag"></param>
    /// <param name="drop"></param>
    public override void HandleDrag(PointerInfo drag, PointerInfo drop)
    {
        hoverImg.enabled = clickImg.enabled = false;

        ColorCube cube = drag.HitGameObject.GetComponent<ColorCube>();

        if (cube != null)
        {
            DropArea dropArea = drop.HitGameObject.GetComponent<DropArea>();

            hoverImg.enabled = clickImg.enabled = false;
            dragImg.enabled = dropImg.enabled = true;

            if (dropArea != null)
            {
                mergeImg.enabled = true;
                dragImg.color = cube.Color;
                dropImg.color = dropArea.Color;
                mergeImg.color = dropArea.calcTargetColor(cube.Color);
            }
            else
            {
                Renderer rend = drop.HitGameObject.GetComponent<Renderer>();
                mergeImg.enabled = false;
                dragImg.color = cube.Color;

                if(rend != null)
                {
                    dropImg.color = rend.material.color;
                }
                else
                {
                    dropImg.color = Color.white * 0.5f;
                }
                
            }
        }
        else
        {
            hoverImg.enabled = clickImg.enabled = dragImg.enabled = dropImg.enabled = mergeImg.enabled = false;
        }

    }

    public override void HandleDrop(PointerInfo drag, PointerInfo drop)
    {

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

    /// <summary>
    /// When holding mouse on Cube, show full circle color preview
    /// </summary>
    /// <param name="info"></param>
    public override void HandleMouseHold(PointerInfo info)
    {
        ColorCube cube = info.HitGameObject.GetComponent<ColorCube>();

        if (cube != null)
        {
            hoverImg.enabled = clickImg.enabled = true;
            clickImg.color = hoverImg.color = cube.Color;
            dragImg.enabled = dropImg.enabled = mergeImg.enabled = false;
        }
        else
        {
            hoverImg.enabled = clickImg.enabled = false;
        }

        
    }

    /// <summary>
    /// When hovering cube show half circle with color preview
    /// Otherwise hide UI
    /// </summary>
    /// <param name="info">Info about mouse pointer</param>
    public override void HandleMouseHover(PointerInfo info)
    {
        ColorCube cube = info.HitGameObject.GetComponent<ColorCube>();

        if (cube != null)
        {
            hoverImg.enabled = true;
            clickImg.enabled = mergeImg.enabled = dragImg.enabled = dropImg.enabled = false;

            hoverImg.color = cube.Color;
        }
        else
        {
            hoverImg.enabled = clickImg.enabled = mergeImg.enabled = dragImg.enabled = dropImg.enabled = false;
        }

    }

    public override void HandleMouseUp(PointerInfo info)
    {

    }
}
