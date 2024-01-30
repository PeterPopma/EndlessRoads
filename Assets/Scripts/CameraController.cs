using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    const float MAX_MOVEMENT_SPEED = 1000;
    bool moveScreenLeft;
    bool moveScreenRight;
    bool moveScreenUp;
    bool moveScreenDown;
    bool zoomIn;
    bool zoomOut;
    bool increaseAngleX;
    bool decreaseAngleX;
    new Camera camera;
    private float cameraAngleX = 60;
    private float cameraAngleY = 0;
    private float cameraHeight = 20;
    private Vector2 moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        camera.transform.rotation = Quaternion.Euler(cameraAngleX, cameraAngleY, 0);
        camera.transform.position = new Vector3(camera.transform.position.x, cameraHeight, camera.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveScreenLeft || moveScreenRight)
        {
            moveSpeed.x = IncreaseMoveSpeed(moveSpeed.x);
        }
        else
        {
            moveSpeed.x = DecreaseMoveSpeed(moveSpeed.x);
        }
        if (moveScreenUp || moveScreenDown)
        {
            moveSpeed.y = IncreaseMoveSpeed(moveSpeed.y);
        }
        else
        {
            moveSpeed.y = DecreaseMoveSpeed(moveSpeed.y);
        }
        camera.transform.position -= new Vector3(Time.deltaTime * moveSpeed.x, 0, Time.deltaTime * moveSpeed.y);

        if (increaseAngleX && cameraAngleX < 89)
        {
            cameraAngleX += 40 * Time.deltaTime;
            camera.transform.rotation = Quaternion.Euler(cameraAngleX, cameraAngleY, 0);
        }
        if (decreaseAngleX && cameraAngleX > 10)
        {
            cameraAngleX -= 40 * Time.deltaTime;
            camera.transform.rotation = Quaternion.Euler(cameraAngleX, cameraAngleY, 0);
        }
        if (zoomIn)
        {
            if (cameraHeight > 0.5f)
            {
                cameraHeight -= Time.deltaTime * 30f;
                camera.transform.position = new Vector3(camera.transform.position.x, cameraHeight, camera.transform.position.z);
            }
        }
        if (zoomOut)
        {
            if (cameraHeight < 300)
            {
                cameraHeight += Time.deltaTime * 30f;
                camera.transform.position = new Vector3(camera.transform.position.x, cameraHeight, camera.transform.position.z);
            }
        }
    }

    private float IncreaseMoveSpeed(float moveSpeed)
    {
        if (moveSpeed > -MAX_MOVEMENT_SPEED && moveSpeed < MAX_MOVEMENT_SPEED)
        {
            moveSpeed *= 1.01f;
        }

        return moveSpeed;
    }

    private float DecreaseMoveSpeed(float moveSpeed)
    {
        moveSpeed *= 0.99f;

        return moveSpeed;
    }

    public void OnScreenLeft(InputValue value)
    {
        if (value.isPressed)
        {
            if (!moveScreenRight)
            {
                moveScreenLeft = true;
                moveSpeed.x = 10;
            }
        }
        else
        {
            moveScreenLeft = false;
        }
    }

    public void OnScreenRight(InputValue value)
    {
        if (value.isPressed)
        {
            if (!moveScreenLeft)
            {
                moveScreenRight = true;
                moveSpeed.x = -10;
            }
        }
        else
        {
            moveScreenRight = false;
        }
    }

    public void OnScreenUp(InputValue value)
    {
        if (value.isPressed)
        {
            if (!moveScreenDown)
            {
                moveScreenUp = true;
                moveSpeed.y = -10;
            }
        }
        else
        {
            moveScreenUp = false;
        }
    }

    public void OnScreenDown(InputValue value)
    {
        if (value.isPressed)
        {
            if (!moveScreenUp)
            {
                moveScreenDown = true;
                moveSpeed.y = 10;
            }
        }
        else
        {
            moveScreenDown = false;
        }
    }

    public void OnZoomIn(InputValue value)
    {
        if (value.isPressed)
        {
            zoomIn = true;
        }
        else
        {
            zoomIn = false;
        }
    }

    public void OnZoomOut(InputValue value)
    {
        if (value.isPressed)
        {
            zoomOut = true;
        }
        else
        {
            zoomOut = false;
        }
    }

    public void OnIncreaseAngleX(InputValue value)
    {
        if (value.isPressed)
        {
            increaseAngleX = true;
        }
        else
        {
            increaseAngleX = false;
        }
    }

    public void OnDecreaseAngleX(InputValue value)
    {
        if (value.isPressed)
        {
            decreaseAngleX = true;
        }
        else
        {
            decreaseAngleX = false;
        }
    }
}
