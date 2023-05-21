using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class CameraFollow : NetworkBehaviour
{
    
    public GameObject myCamera;

    void Start()
    {
        GameObject cameraObject = GameObject.Find("Camera");
        if (cameraObject != null)
        {
            Camera camera = cameraObject.GetComponent<Camera>();
            if (!IsOwner && camera != null)
            {
                myCamera.SetActive(false);
            }
        }
    }
    /*
    void Update()
    {
        if (myCamera != null)
        {
            // Do something with the camera
            myCamera.transform.Rotate(Vector3.up, Time.deltaTime * 10f);
        }
    }
    */

    /*
    public GameObject p1Camera;
    public GameObject p2Camera;

    private void Start()
    {
        if (IsOwner)
        {
            p1Camera.SetActive(true);
            p2Camera.SetActive(false);
        }
        else
        {
            p1Camera.SetActive(false);
            p2Camera.SetActive(true);
        }
    }

    private void Update()
    {
        // Rotate the active camera
        GameObject activeCamera = IsOwner ? p1Camera : p2Camera;
        if (activeCamera != null)
        {
            activeCamera.transform.Rotate(Vector3.up, Time.deltaTime * 10f);
        }
    }
    */

    /*
    public GameObject cameraHolder;
    public Vector3 offset;

    public void OnStartAuthority()
    {
        cameraHolder.SetActive(true);
    }


    public void Update()
    {
        cameraHolder.transform.position = transform.position + offset; 
    }
    */
}
