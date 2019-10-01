using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRocket : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().Play("MenuRocket", -1, 0);
    }
}
