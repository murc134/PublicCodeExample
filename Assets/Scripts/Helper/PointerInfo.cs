using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerInfo 
{
    public static Vector3 INVALID { get { return new Vector3(float.NaN, float.NaN, float.NaN); } }
    public bool HasReceiver
    {
        get
        {
            return Receiver != null;
        }
    }
    private MouseEventHandler receiver;
    public MouseEventHandler Receiver
    {
        get
        {
            return receiver;
        }
        private set
        {
            receiver = value;
        }
    }
    public Vector3 OriginalMousePos;
    public Vector3 HitPos { get => hit.point; }
    public Vector3 HitTransformPos { get => hit.transform.position; }
    public GameObject HitGameObject { get => hit.transform.gameObject; }
    public Transform HitTransform { get => hit.transform; }

    private RaycastHit hit;

    private float time;

    private float maxLifetime = 0.5f;

    private string displayname = "";

    private Vector3 mouseMovementDelta = INVALID;

    private static Vector3 previousMousepos = INVALID;

    public Vector3 MouseDelta => mouseMovementDelta == INVALID ? Vector3.zero : mouseMovementDelta;

    public string Displayname
    {
        get
        {
            return string.IsNullOrEmpty(displayname) ? HitTransform.name : displayname;
        }
    }

    public PointerInfo(Vector3 origMousePos, RaycastHit newhit)
    {
        if(previousMousepos == INVALID)
        {
            previousMousepos = origMousePos;
        }

        mouseMovementDelta = previousMousepos - origMousePos;

        Receiver = null;
        OriginalMousePos = origMousePos;
        hit = newhit;
        time = Time.time;

        if(hit.transform == null)
        {
            throw new System.Exception("Hit Cannot be null");
        }
        displayname = hit.transform.name;

        previousMousepos = origMousePos;
    }
    public PointerInfo(MouseEventHandler receiver, Vector3 origMousePos, RaycastHit newhit)
    {
        if (previousMousepos == INVALID)
        {
            previousMousepos = origMousePos;
        }

        mouseMovementDelta = previousMousepos - origMousePos;

        Receiver = receiver;
        OriginalMousePos = origMousePos;
        hit = newhit;
        time = Time.time;
        displayname = hit.transform.name;
        previousMousepos = origMousePos;
    }

    public bool isOutdated
    {
        get { return ((Time.time - time) > maxLifetime); }
    }

    public override string ToString()
    {
        return HitTransform.name;
    }
}
