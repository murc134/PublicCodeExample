using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private List<Material> defaultMaterials = new List<Material>();
    [SerializeField]
    private List<Material> teleportMaterials = new List<Material>();

    [SerializeField]
    private List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();

    [SerializeField]
    private float val = 0.25f;

    /// <summary>
    /// Ranged float value that determines the status of the teleport effect in the shader
    /// </summary>
    public float Value
    {
        get
        {
            return Mathf.Clamp(val, 0.0f, 1.0f);
        }
        set
        {
            val = Mathf.Clamp(value, 0.0f, 1.0f);
        }
    }

    private PointerInfo info;

    public float EffectSpeed = 1;

    private void Awake()
    {
        if (skinnedMeshRenderers == null)
        {
            skinnedMeshRenderers = new List<SkinnedMeshRenderer>();
        }
        if(skinnedMeshRenderers.Count == 0)
        { 
            SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

            if (skinnedMeshRenderer == null)
            {
                Debug.LogError($"No {typeof(SkinnedMeshRenderer).Name} was found on {GetType().Name}", gameObject);
            }
            else
            {
                skinnedMeshRenderers.Add(skinnedMeshRenderer);
            }
        }
    }

    private void Start()
    {
        if (defaultMaterials == null || defaultMaterials.Count == 0)
        {
            Debug.LogError($"DefaultMaterials not set on {GetType().Name}", gameObject);
        }
        else if(defaultMaterials.Count < skinnedMeshRenderers.Count)
        {
            Debug.LogError($"DefaultMaterials on {GetType().Name} do not match count of {typeof(SkinnedMeshRenderer)} array", gameObject);
        }
        if (teleportMaterials == null || teleportMaterials.Count == 0)
        {
            Debug.LogError($"TeleportMaterials not set on {GetType().Name}", gameObject);
        }
        else if (teleportMaterials.Count < skinnedMeshRenderers.Count)
        {
            Debug.LogError($"TeleportMaterials on {GetType().Name} do not match count of {typeof(SkinnedMeshRenderer)} array", gameObject);
        }
    }

    private void Update()
    {
        for(int j=0; j< skinnedMeshRenderers.Count;j++)
        {
            Material[] mats = skinnedMeshRenderers[j].sharedMaterials;

            for (int i = 0; i < mats.Length; i++)
            {
                if (Value == 0)
                {
                    if (mats[i] == teleportMaterials[j])
                    {
                        mats[i] = defaultMaterials[j];
                    }
                }
                else
                {
                    if (mats[i] == defaultMaterials[j])
                    {
                        mats[i] = teleportMaterials[j];
                    }
                    skinnedMeshRenderers[j].sharedMaterials[i].SetFloat("_TeleportEffect", val);
                }
            }
            skinnedMeshRenderers[j].sharedMaterials = mats;
            
        }


        Teleport();
    }

    public void Teleport(PointerInfo nfo)
    {
        info = nfo;
    }


    private void Teleport()
    {
        if (info != null)
        {
            if (Vector3.Distance(transform.position, info.HitPos) > 0.5f)
            {

                Value += EffectSpeed * Time.deltaTime;
                if (Value >= 1)
                {
                    transform.position = info.HitPos;
                }

            }
            else
            {
                // Then decrease until fully visible
                Value -= EffectSpeed * Time.deltaTime;
                if (Value <= 0.2f)
                {
                    Value = 0;
                    info = null;
                }
            }
        }
    }


}
