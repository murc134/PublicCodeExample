using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An area, where <see cref="ColorCube"/> can be dropped that will then merge the color of the cube into its own color
/// </summary>
public class DropArea : MouseEventHandler
{
    [SerializeField]
    private ColorCube prefabred, prefabgreen, prefabblue;

    private ColorCube instancered, instancegreen, instanceblue;

    private MeshRenderer meshrenderer;

    /// <summary>
    /// Color of the area
    /// </summary>
    public Color Color
    {
        get
        {
            return meshrenderer.material.GetColor("_Color");
        }
        private set
        {
            meshrenderer.material.SetColor("_Color", value);
        }
    }

    private void Awake()
    {
        if (prefabred == null) Debug.LogError($"Missing prefabred on {GetType().Name}", this);
        if (prefabgreen == null) Debug.LogError($"Missing prefabgreen on {GetType().Name}", this);
        if (prefabblue == null) Debug.LogError($"Missing prefabblue on {GetType().Name}", this);

        meshrenderer = GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetDropArea();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Reset color cube instances
    /// If not existing it will create color cube instances. 
    /// </summary>
    private void ResetDropArea()
    {
        createInstance(in prefabred, ref instancered, 2 * transform.forward);
        createInstance(in prefabgreen, ref instancegreen, 2 * (transform.forward + transform.right));
        createInstance(in prefabblue, ref instanceblue, 2 * (transform.forward - transform.right));
    }

    /// <summary>
    /// Creates color cube instance if not existing
    /// Otherwise reset color cube instance to default values
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="instance"></param>
    /// <param name="position"></param>
    private void createInstance(in ColorCube prefab, ref ColorCube instance, Vector3 position)
    {
        if (instance == null)
        {
            instance = Instantiate<ColorCube>(prefab);
            instance.transform.position = position;
        }
        else
        {
            instance.ResetPosition();
            instance.ResetColor();
        }
    }

    /// <summary>
    /// On single click reset cubes
    /// </summary>
    /// <param name="info"></param>
    public override void HandleMouseClick1(PointerInfo info)
    {
        if(info.HitGameObject == gameObject)
        {
            ResetDropArea();
        }
    }

    /// <summary>
    /// On double click reset drop area color
    /// </summary>
    /// <param name="info"></param>
    public override void HandleMouseClick2(PointerInfo info)
    {
        if (info.HitGameObject == gameObject)
        {
            Color = Color.black;
        }
    }

    public override void HandleMouseDown(PointerInfo info)
    {
        
    }

    public override void HandleMouseUp(PointerInfo info)
    {
        
    }

    public override void HandleMouseHold(PointerInfo info)
    {
        
    }

    public override void HandleDrag(PointerInfo drag, PointerInfo drop)
    {
        
    }
    public override void HandleMouseHover(PointerInfo info)
    {

    }

    /// <summary>
    /// WHen dropping <see cref="ColorCube"/> reset its position and randomize its color
    /// </summary>
    /// <param name="drag"></param>
    /// <param name="drop"></param>
    public override void HandleDrop(PointerInfo drag, PointerInfo drop)
    {
        if(drop.HitTransform == transform)
        {
            ColorCube cube = drag.HitGameObject.GetComponent<ColorCube>();

            if (cube != null)
            {
                Color = calcTargetColor(cube.Color);
                cube.ResetPosition();
                cube.RandomColor();
            }
        }
        
    }

    /// <summary>
    /// Used to calculate the drop area target color
    /// </summary>
    /// <param name="c"></param>
    /// <returns>Calculated Color</returns>
    public Color calcTargetColor(Color c)
    {
        return Color.Lerp(Color, c, 0.5f);
    }
}
