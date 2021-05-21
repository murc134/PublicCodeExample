using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeIndicator : MouseEventHandler
{
    [SerializeField]
    private Image clickImg, hoverImg, mergeImg, dragImg, dropImg;

    //long i = 0;

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
                mergeImg.color = dropArea.calcTargetColor(dropArea.Color + cube.Color * dropArea.mergeMultiplier);
                //Debug.Log($"merge {i++}");
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
            //Debug.Log($"merge off {i++}");
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

    public override void HandleMouseHold(PointerInfo info)
    {
        ColorCube cube = info.HitGameObject.GetComponent<ColorCube>();

        if (cube != null)
        {
            hoverImg.enabled = clickImg.enabled = true;
            clickImg.color = hoverImg.color = cube.Color;
            dragImg.enabled = dropImg.enabled = mergeImg.enabled = false;
            //Debug.Log($"merge off {i++}");
        }
        else
        {
            hoverImg.enabled = clickImg.enabled = false;
        }

        
    }
    public override void HandleMouseHover(PointerInfo info)
    {
        ColorCube cube = info.HitGameObject.GetComponent<ColorCube>();

        if (cube != null)
        {
            hoverImg.enabled = true;
            clickImg.enabled = mergeImg.enabled = dragImg.enabled = dropImg.enabled = false;
            //Debug.Log($"merge off {i++}");

            hoverImg.color = cube.Color;
        }
        else
        {
            hoverImg.enabled = clickImg.enabled = mergeImg.enabled = dragImg.enabled = dropImg.enabled = false;
            //Debug.Log($"merge off {i++}");
        }

    }

    public override void HandleMouseUp(PointerInfo info)
    {

    }
}
