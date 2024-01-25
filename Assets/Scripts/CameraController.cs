using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    bool moveScreenLeft;
    bool moveScreenRight;
    bool moveScreenUp;
    bool moveScreenDown;
    bool zoomIn;
    bool zoomOut;
    bool increaseAngleX;
    bool decreaseAngleX;
    new Camera camera;
    private float timeMovingStarted;
    private float cameraAngleX = 60;
    private float cameraAngleY = 0;
    private float cameraHeight = 20;

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
        float moveSpeed = 24f * Mathf.Pow(3, (Time.time - timeMovingStarted));
        if (moveSpeed>2000f)
        {
            moveSpeed = 2000f;
        }
        if (moveScreenLeft)
        {
            camera.transform.position -= new Vector3(Time.deltaTime * moveSpeed, 0, 0);
        }
        if (moveScreenRight)
        {
            camera.transform.position += new Vector3(Time.deltaTime * moveSpeed, 0, 0);
        }
        if (moveScreenUp)
        {
            camera.transform.position += new Vector3(0, 0, Time.deltaTime * moveSpeed);
        }
        if (moveScreenDown)
        {
            camera.transform.position -= new Vector3(0, 0, Time.deltaTime * moveSpeed);
        }
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
                cameraHeight -= Time.deltaTime * 10f;
                camera.transform.position = new Vector3(camera.transform.position.x, cameraHeight, camera.transform.position.z);
            }
        }
        if (zoomOut)
        {
            if (cameraHeight < 300)
            {
                cameraHeight += Time.deltaTime * 10f;
                camera.transform.position = new Vector3(camera.transform.position.x, cameraHeight, camera.transform.position.z);
            }
        }
    }

    public void OnScreenLeft(InputValue value)
    {
        timeMovingStarted = Time.time;
        moveScreenLeft = value.isPressed;
    }

    public void OnScreenRight(InputValue value)
    {
        timeMovingStarted = Time.time;
        moveScreenRight = value.isPressed;
    }

    public void OnScreenUp(InputValue value)
    {
        timeMovingStarted = Time.time;
        moveScreenUp = value.isPressed;
    }

    public void OnScreenDown(InputValue value)
    {
        timeMovingStarted = Time.time;
        moveScreenDown = value.isPressed;
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
