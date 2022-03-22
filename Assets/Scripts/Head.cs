using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private Transform rootObject, followObject;
    [SerializeField] private Vector3 positionOffset, rotationOffset, headBodyOffset;

    //private Vector3 ;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = rootObject.position - followObject.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        rootObject.position = transform.position + headBodyOffset;
        Vector3 forwardDir = Vector3.ProjectOnPlane(followObject.up, Vector3.up);
        if (forwardDir.magnitude > 0)
            rootObject.forward = forwardDir.normalized;

        transform.position = followObject.TransformPoint(positionOffset);
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    }
}
