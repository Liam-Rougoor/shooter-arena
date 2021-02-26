using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera.transform.position);
        transform.Rotate(0, 180, 0);
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 3f;
    }
}
