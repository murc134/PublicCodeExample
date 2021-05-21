using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Very basic class to change info text on Drop Area to explain mouse events
/// </summary>
public class InfoTextHandler : MouseEventHandler
{
    const string defaultString = "Drag & Drop colored cube into this area";
    const string hoverCube = "Drag & Drop colored cube into this area";
    const string hoverDropArea = "Double Click to reset color";
    const string onDragCube = "Now drag the cube into this area";
    const string onDropEnable = "Now drop the cube";
    const string onDropDisable = "Cannot drop here";

    [SerializeField]
    private TextMeshProUGUI text;

    public override void HandleDrag(PointerInfo drag, PointerInfo drop)
    {
        ColorCube cube = drag.HitGameObject.GetComponent<ColorCube>();

        if(cube != null)
        {
            DropArea area = drop.HitGameObject.GetComponent<DropArea>();

            if(area != null)
            {
                text.text = onDropEnable;
            }
            else
            {
                text.text = onDragCube;
            }
        }
        else
        {
            text.text = onDropDisable;
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
        ColorCube cube = info.HitGameObject.GetComponent<ColorCube>();
        if(cube!=null)
        {
            text.text = onDragCube;
        }
    }

    public override void HandleMouseHold(PointerInfo info)
    {

    }

    public override void HandleMouseHover(PointerInfo info)
    {
        ColorCube cube = info.HitGameObject.GetComponent<ColorCube>();
        DropArea area = info.HitGameObject.GetComponent<DropArea>();

        if(cube != null)
        {
            text.text = hoverCube;
        }
        else if(area != null)
        {
            text.text = hoverDropArea;
        }
        else
        {
            text.text = defaultString;
        }
    }

    public override void HandleMouseUp(PointerInfo info)
    {

    }
}
