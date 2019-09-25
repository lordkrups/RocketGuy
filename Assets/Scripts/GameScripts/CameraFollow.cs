using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameSceneManagerNGUI gameSceneManagerNGUI;
    public Vector3 offSet;
    public bool isCamera;
    public bool isBG;
    public bool isFloor;
    // Start is called before the first frame update
    void Start()
    {
        offSet = transform.position - gameSceneManagerNGUI.playerRocket.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (isFloor)
        {
            offSet.x = gameSceneManagerNGUI.playerRocket.transform.position.x;
            offSet.y = transform.position.y;
            offSet.z = transform.position.z;
            transform.position = offSet;
        }
        if (isBG)
        {
            offSet.x = gameSceneManagerNGUI.playerRocket.transform.position.x;
            offSet.y = gameSceneManagerNGUI.playerRocket.transform.position.y;
            offSet.z = transform.position.z;
            transform.position = offSet;
        }
        if (isCamera)
        {
            transform.position = gameSceneManagerNGUI.playerRocket.transform.position + offSet;
        }
    }
}
