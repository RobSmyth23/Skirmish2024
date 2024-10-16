using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_CameraScript : MonoBehaviour
{
    float minHeight = 5f;
    float maxHeight = 50f;
    float zoomStep = 2;
    private float cameraSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 20, 20);  // starting Camera Position
        transform.LookAt(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {

        if (shouldMoveIn()) zoomIn();
        if (shouldMoveOut()) zoomOut();

        mouseRotate();


        if (shouldMoveForward()) moveForward();
        if (shouldMoveBack()) moveBack();
        if (shouldMoveLeft()) moveLeft();
        if (shouldMoveRight()) moveRight();
    }

    private void moveRight()
    {
        transform.position += cameraSpeed * transform.right * Time.deltaTime;
    }

    private bool shouldMoveRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    private void moveLeft()
    {
        transform.position -= cameraSpeed * transform.right * Time.deltaTime;
    }

    private bool shouldMoveLeft()
    {
       return  Input.GetKey(KeyCode.A);
    }

    private void mouseRotate()
    {  
        if (Input.GetMouseButton(0))
        {
            Quaternion rotation = transform.rotation;
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal"), Space.World);
            transform.Rotate(Vector3.right, Input.GetAxis("Vertical"), Space.Self);
            if (hasRotatedTooFar())
                transform.rotation = rotation;
        }
    }

    private bool hasRotatedTooFar()
    {
        return !((Vector3.Dot(transform.forward, Vector3.down) >= 0) &&
           (transform.up.y >= 0));
        
    }

    private void moveBack()
    {
        Vector3 dir = (new Vector3(transform.forward.x, 0, transform.forward.z)).normalized;
        transform.position -= cameraSpeed * dir * Time.deltaTime;

    }

    private bool shouldMoveBack()
    {
        return Input.GetKey(KeyCode.S);
    }

    private void moveForward()
    {
        Vector3 dir = (new Vector3(transform.forward.x, 0, transform.forward.z)).normalized;
        transform.position += cameraSpeed *dir * Time.deltaTime;
    }

    private void turnRight()
    {
        throw new NotImplementedException();
    }

    private void turnLeft()
    {
        throw new NotImplementedException();
    }

    private void zoomOut()
    {
        transform.position -= zoomStep * transform.forward;
        ClampHeight();
    }



    private void zoomIn()
    {
        transform.position += zoomStep * transform.forward;
        ClampHeight();
    }

    private bool shouldMoveForward()
    {
        return Input.GetKey(KeyCode.W);
    }

    private bool shouldTurnRight()
    {
        throw new NotImplementedException();
    }

    private bool shouldTurnLeft()
    {
        throw new NotImplementedException();
    }

    private bool shouldMoveOut()
    {
        return (Input.mouseScrollDelta.y < 0);
    }

    private bool shouldMoveIn()
    {
        return (Input.mouseScrollDelta.y > 0);
    }

    private void ClampHeight()
    {
        transform.position = new Vector3(transform.position.x,
                                        Mathf.Clamp(transform.position.y, minHeight, maxHeight),
                                        transform.position.z);
    }
}
