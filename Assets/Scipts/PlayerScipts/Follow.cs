using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.GetChild(1).position;
    }
}
