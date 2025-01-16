using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    void Awake()
    {

    }
    void Start()
    {
        GameObject mainCamera = GameObject.Find("Manin Camera");
        Camera.main.orthographicSize = (520 * (16f / 9f) / 2) / 100;
        Camera.main.aspect = 9f / 16f;
    }
}
