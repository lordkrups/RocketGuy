using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public Transform modelTransform;
    public Quaternion offsetRot;

    public bool lookLeft;

    // Start is called before the first frame update
    void Start()
    {
        if (lookLeft)
        {
            Quaternion deltaRotation = Quaternion.Euler(0, 90f, 0);
            transform.rotation = deltaRotation;
        }
        else
        {
            Quaternion deltaRotation = Quaternion.Euler(0, 270, 0);
            transform.rotation = deltaRotation;
        }        
    }

}
