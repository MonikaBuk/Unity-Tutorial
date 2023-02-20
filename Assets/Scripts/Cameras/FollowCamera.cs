using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;
    private float mouseX;
    private float mouseY;
    private float mouseZ;



    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void LateUpdate()
    {
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0); 
        transform.position = target.transform.position + (rotation * offset);
        transform.LookAt(target.transform);
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mouseZ = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(1))
        {
            offset = Quaternion.Euler(0, mouseX, 0) * offset;
        }
        if (Input.GetMouseButton(0))
        {
            offset = Quaternion.Euler(mouseY, 0, 0) * offset;
        }
    }

   
}
