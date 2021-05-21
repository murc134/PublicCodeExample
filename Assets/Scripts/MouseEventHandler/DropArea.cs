using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MouseEventHandler
{
    [SerializeField]
    private ColorCube prefabred, prefabgreen, prefabblue;

    private ColorCube instancered, instancegreen, instanceblue;

    private MeshRenderer meshrenderer;

    public float mergeMultiplier = 0.1f;

    public Color Color
    {
        get
        {
            return meshrenderer.material.GetColor("_Color");
        }
        private set
        {
            meshrenderer.material.SetColor("_Color", calcTargetColor(value));
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

    private void ResetDropArea()
    {
        createInstance(in prefabred, ref instancered, 2 * transform.forward);
        createInstance(in prefabgreen, ref instancegreen, 2 * (transform.forward + transform.right));
        createInstance(in prefabblue, ref instanceblue, 2 * (transform.forward - transform.right));


    }

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
        }
    }

    public override void HandleMouseClick1(PointerInfo info)
    {
        if(info.HitGameObject == gameObject)
        {
            ResetDropArea();
        }
    }

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
    public override void HandleDrop(PointerInfo drag, PointerInfo drop)
    {
        if(drop.HitTransform == transform)
        {
            ColorCube cube = drag.HitGameObject.GetComponent<ColorCube>();

            if (cube != null)
            {
                Color += cube.Color * mergeMultiplier;
                cube.ResetPosition();
            }
        }
        
    }

    public Color calcTargetColor(Color c)
    {
        if (c.r > 1) { c.r = c.r - 1; }
        if (c.g > 1) { c.g = c.g - 1; }
        if (c.b > 1) { c.b = c.b - 1; }

        return c;
    }
}
