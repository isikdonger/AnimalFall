using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.parent.childCount == 2)
        {
            transform.position = transform.parent.GetChild(1).position;
        }
    }
}
