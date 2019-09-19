using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUMenuObj : MonoBehaviour
{
    public UISprite mainSprite;

    public void DestroyMenuObj()
    {
        Destroy(this);
    }
}
