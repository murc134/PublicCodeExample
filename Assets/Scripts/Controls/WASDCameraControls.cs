
using UnityEngine;


public class WASDCameraControls : MonoBehaviour
{
    public float Speed = 1;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    [SerializeField]
    private LayerMask BlockingLayers;

    private Vector3 previousMousePosition = Vector3.zero;

    private void Update()
    {
        float y = transform.position.y;

        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            handleKeyMovement(Input.GetAxis("Horizontal"), transform.right);
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            handleKeyMovement(Input.GetAxis("Vertical"), transform.forward);
        }

        pitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetButton($"Fire1"))
            {
                Debug.Log($"Fire1 Shift");
                handleMouseMovement();
            }
            else if(Input.GetButton($"Fire2"))
            {
                Debug.Log($"Fire2 Shift");
                rotateCamera();
            }
        }
        else if (Input.GetButton($"Fire2"))
        {
            Debug.Log("Fire2");
            rotateCamera();
        }
        else if (Input.GetButton($"Fire3"))
        {
            Debug.Log($"Fire3");
            handleMouseMovement();
            // move according to mousedelte
        }

        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos;

        previousMousePosition = Input.mousePosition;
    }

    private void rotateCamera()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
    private void handleMouseMovement()
    {
        Vector3 dir = previousMousePosition - Input.mousePosition;
        dir.z = dir.y;
        dir.y = 0;

        Ray ray = new Ray(transform.position, dir * Speed);

        if (Physics.Raycast(ray, 1, BlockingLayers))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
            transform.Translate(dir * Speed * Time.deltaTime, Space.Self);
        }


    }
    private void handleKeyMovement(float input, Vector3 direction)
    {
        if (Mathf.Abs(input) > 0)
        {
            Ray ray = new Ray(transform.position, direction * input * Speed);

            if (Physics.Raycast(ray, 1, BlockingLayers))
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.green);
                transform.Translate(direction * input * Speed * Time.deltaTime, Space.World);
            }

        }
    }
}

