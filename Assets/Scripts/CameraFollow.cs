using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform lookTarget;
    public Vector3 offSet;
    public bool isCamera;
    public bool isBG;
    public bool isFloor;
    // Start is called before the first frame update
    void Start()
    {
        offSet = transform.position - lookTarget.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (isFloor)
        {
            offSet.x = lookTarget.position.x;
            offSet.y = transform.position.y;
            offSet.z = lookTarget.position.z;
            transform.position = offSet;
        }
        if (isBG)
        {
            offSet.x = lookTarget.position.x;
            offSet.y = lookTarget.position.y;
            offSet.z = transform.position.z;
            transform.position = offSet;
        }
        if (isCamera)
        {
            transform.position = lookTarget.position + offSet;
        }
    }
}
